using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace NetworkScanner
{
    public partial class NetworkScanner : Form
    {
        public NetworkScanner()
        {
            InitializeComponent();
        }

        private void NetworkScanner_Load(object sender, EventArgs e)
        {
            #region Lấy ra tất cả Network Interface khả dụng
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    interfacesComboBox.Items.Add(networkInterface.Name);
            }
            #endregion
        }

        private void interfacesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Lấy ra thông tin của Interface khi được chọn
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.Name.Contains(interfacesComboBox.Text))    // Lấy ra Interface được chọn
                {
                    IPInterfaceProperties properties = networkInterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddressTextBox.Text = ip.Address.ToString();
                            subnetMaskTextBox.Text = ip.IPv4Mask.ToString();

                            IPAddress subnetMask = ip.IPv4Mask;
                            byte[] subnetBytes = subnetMask.GetAddressBytes();
                            byte[] ipBytes = ip.Address.GetAddressBytes();

                            // Tìm địa chỉ mạng bằng cách AND giữa địa chỉ IP và subnet mask
                            byte[] networkAddress = new byte[ipBytes.Length];
                            for (int i = 0; i < ipBytes.Length; i++)
                            {
                                networkAddress[i] = (byte)(ipBytes[i] & subnetBytes[i]);
                            }

                            networkTextBox.Text = new IPAddress(networkAddress).ToString();
                        }
                    }
                    break;
                }
            }
            #endregion

            findButton.Enabled = true;  // Thay đổi trạng thái của nút Quét
        }

        CancellationTokenSource cancellationTokenSource_1;
        CancellationToken cancellationToken_1;
        private async void findButton_Click(object sender, EventArgs e)
        {
            interfacesComboBox.Enabled = false;
            findButton.Enabled = false;
            stopFindingButton.Enabled = true;
            listView.Items.Clear();

            cancellationTokenSource_1 = new CancellationTokenSource();
            cancellationToken_1 = cancellationTokenSource_1.Token;
            Task Find = FindIPAddresses();
            await Find;

            interfacesComboBox.Enabled = true;
            findButton.Enabled = true;
            stopFindingButton.Enabled = false;
            progessPercent.Text = string.Empty;

        }

        private void stopFindingButton_Click(object sender, EventArgs e)
        {
            cancellationTokenSource_1.Cancel();
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            seletedIpAddressTextBox.Text = e.Item.Text;
        }

        private async Task FindIPAddresses()
        {
            #region Tính toán địa chỉ boardcast
            string networkAddressString = string.Empty;
            string subnetMaskString = string.Empty;
            networkTextBox.Invoke(new MethodInvoker(() =>
            {
                networkAddressString = networkTextBox.Text;
            }));
            subnetMaskTextBox.Invoke(new MethodInvoker(() =>
            {
                subnetMaskString = subnetMaskTextBox.Text;
            }));

            IPAddress ip = System.Net.IPAddress.Parse(networkAddressString);
            IPAddress mask = System.Net.IPAddress.Parse(subnetMaskString);

            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

            byte[] networkAddress = new byte[ipBytes.Length];
            for (int i = 0; i < networkAddress.Length; i++)
            {
                networkAddress[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }

            byte[] broadcastAddress = new byte[ipBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipBytes[i] | (maskBytes[i] ^ 255));
            }
            #endregion

            // Tính toán thanh trạng thái
            pingTaskProgressBar.Value = 0;
            pingTaskProgressBar.Maximum = (broadcastAddress[0] - networkAddress[0] + 1) * (broadcastAddress[1] - networkAddress[1] + 1) * (broadcastAddress[2] - networkAddress[2] + 1) * (broadcastAddress[3] - networkAddress[3] + 1);

            Task pingEachIpAddress = new Task(async () =>
            {
                // Tạo ra danh sách Task
                List<Task> pingTasks = new List<Task>();

                for (int i = networkAddress[0]; i <= broadcastAddress[0]; i++)
                {
                    for (int j = networkAddress[1]; j <= broadcastAddress[1]; j++)
                    {
                        for (int k = networkAddress[2]; k <= broadcastAddress[2]; k++)
                        {
                            for (int l = networkAddress[3]; l <= broadcastAddress[3]; l++)
                            {
                                // Kiểm tra có token huỷ hay không
                                if (cancellationToken_1.IsCancellationRequested)
                                {
                                    return;
                                }

                                // Địa chỉ IP mục tiêu
                                IPAddress target = System.Net.IPAddress.Parse($"{i}.{j}.{k}.{l}");

                                // Tạo ra 1 task cho mỗi IP cần ping và thêm vào danh sách Task
                                pingTasks.Add(pingTask(target));
                            }
                        }
                    }
                }

                // Chờ tất cả các pingTask được hoàn tất
                Task.WaitAll(pingTasks.ToArray());


            }, cancellationToken_1);
            pingEachIpAddress.Start();
            await pingEachIpAddress;
        }

        private async Task pingTask(IPAddress target)
        {
            // Kiểm tra có token huỷ hay không
            if (cancellationToken_1.IsCancellationRequested)
            {
                return;
            }

            // Ping đến IP mục tiêu
            Ping myPing = new Ping();
            PingReply pingReply;
            pingReply = await myPing.SendPingAsync(target);

            //Ping thành công
            if (pingReply.Status == IPStatus.Success)
            {
                listView.Invoke(() =>
                {
                    listView.Items.Add(new ListViewItem(new string[] { target.ToString(), GetOSName(pingReply.Options.Ttl) }));
                });

            }

            // Cập nhật thanh quá trình
            int progressMaxValue = 0;
            int progressCurrentValue = 0;
            pingTaskProgressBar.Invoke(() =>
            {
                pingTaskProgressBar.Value += 1;
                progressMaxValue = pingTaskProgressBar.Maximum;
                progressCurrentValue = pingTaskProgressBar.Value;
            });

            // Cập nhật tiến độ
            progessPercent.Invoke(() =>
            {
                double percent = (double)progressCurrentValue / (double)progressMaxValue * 100;
                progessPercent.Text = $"{Math.Round(percent, 2)}%";
            });
            if (cancellationToken_1.IsCancellationRequested)
            {
                progessPercent.Invoke(() =>
                {
                    progessPercent.Text = string.Empty;
                });
            }
        }

        #region Danh sách hệ điều hành
        private string GetOSName(int TTL)
        {
            switch (TTL)
            {
                case 64:
                    return "Linux/MacOS/Other";
                case 128:
                    return "Window";
                case 255:
                    return "Network devices";
                default:
                    return "Can not identifying...";
            }
        }
        #endregion

        private void seletedIpAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (seletedIpAddressTextBox.Text != string.Empty)
            {
                featureComboBox.Enabled = true;
            }
            else
            {
                featureComboBox.Enabled = false;
            }
        }

        private void featureComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (featureComboBox.SelectedIndex != -1)
            {
                startButton.Enabled = true;
            }
            else
            {
                startButton.Enabled = false;
            }
        }

        CancellationTokenSource cancellationTokenSource_2;
        CancellationToken cancellationToken_2;
        Account accountForm;
        private async void startButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                IPAddress target = System.Net.IPAddress.Parse(seletedIpAddressTextBox.Text);
                cancellationTokenSource_2 = new CancellationTokenSource();
                cancellationToken_2 = cancellationTokenSource_2.Token;
                if (featureComboBox.Text == "Quét cổng TCP")
                {
                    startButton.Enabled = false;
                    stopButton.Enabled = true;

                    logRichTextBox.AppendText($"\r\n---------- Quét cổng của địa chỉ {seletedIpAddressTextBox.Text} ----------\r\n");
                    Task Scan = PortScan(target);
                    await Scan;

                    startButton.Enabled = true;
                    stopButton.Enabled = false;
                }
                else if (featureComboBox.Text == "Tìm thông tin (chỉ hỗ trợ Windows)")
                {
                    accountForm = new Account();
                    accountForm.ShowDialog();
                    if (accountForm.IsOK == true)
                    {
                        startButton.Enabled = false;
                        stopButton.Enabled = true;

                        logRichTextBox.AppendText($"\r\n---------- Tìm thông tin thiết bị của địa chỉ {seletedIpAddressTextBox.Text} ----------\r\n");
                        Task findinfo = FindInfo(target, accountForm.username, accountForm.password);
                        await findinfo;

                        startButton.Enabled = true;
                        stopButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stopButton_Click(Object sender, EventArgs e)
        {
            cancellationTokenSource_2.Cancel();
        }

        private async Task PortScan(IPAddress target)
        {
            Dictionary<int, KeyValuePair<string, string>> ListOfCommonPorts = portList();

            List<int> ports = ListOfCommonPorts.Keys.ToList();

            Task scanTask = new Task(() =>
            {
                List<Task> portScanTasks = new List<Task>();
                foreach (int port in ports)
                {
                    if (cancellationToken_2.IsCancellationRequested)
                    {
                        return;
                    }
                    KeyValuePair<string, string> keyValuePair = ListOfCommonPorts[port];
                    string service = keyValuePair.Key;
                    string protocol = keyValuePair.Value;
                    
                    portScanTasks.Add(TcpPortScan(target, port, service, protocol));
                }
                Task.WaitAll(portScanTasks.ToArray());
            }, cancellationToken_2);
            scanTask.Start();
            await scanTask;
        }

        #region Danh sách các cổng phổ biến
        private Dictionary<int, KeyValuePair<string, string>> portList()
        {
            Dictionary<int, KeyValuePair<string, string>> portInfo = new Dictionary<int, KeyValuePair<string, string>>();
            portInfo.Add(20, new KeyValuePair<string, string>("FTP data transfer", "TCP/UDP"));
            portInfo.Add(21, new KeyValuePair<string, string>("FTP command control", "TCP/UDP"));
            portInfo.Add(22, new KeyValuePair<string, string>("SSH/SCP/SFTP", "TCP/UDP"));
            portInfo.Add(23, new KeyValuePair<string, string>("Telnet", "TCP"));
            portInfo.Add(53, new KeyValuePair<string, string>("DNS", "TCP/UDP"));
            portInfo.Add(80, new KeyValuePair<string, string>("HTTP", "TCP/UDP"));
            portInfo.Add(110, new KeyValuePair<string, string>("POP3", "TCP"));
            portInfo.Add(135, new KeyValuePair<string, string>("Microsoft EPMAP", "TCP/UDP"));
            portInfo.Add(137, new KeyValuePair<string, string>("NetBIOS", "TCP/UDP"));
            portInfo.Add(143, new KeyValuePair<string, string>("IMAP", "TCP/UDP"));
            portInfo.Add(162, new KeyValuePair<string, string>("SNMPTRAP", "TCP/UDP"));
            portInfo.Add(443, new KeyValuePair<string, string>("HTTPS", "TCP/UDP"));
            portInfo.Add(445, new KeyValuePair<string, string>("Microsoft Directory Services", "TCP/UDP"));
            portInfo.Add(993, new KeyValuePair<string, string>("IMAPS", "TCP"));
            portInfo.Add(3389, new KeyValuePair<string, string>("RDP", "TCP"));
            return portInfo;
        }
        #endregion

        private async Task TcpPortScan(IPAddress target, int port, string service, string protocol)
        {
            Task Scan = new Task(() =>
            {
                TcpClient tcpClient = new TcpClient();
                try
                {
                    var result = tcpClient.BeginConnect(target, port, null, null);
                    if (result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1)))
                    {
                        logRichTextBox.Invoke(() =>
                        {
                            logRichTextBox.AppendText($"\r\nCổng: {port}\r\nGiao thức: {protocol}\r\nDịch vụ: {service}\r\n");
                        });
                    }
                }
                catch
                {

                }
            });
            Scan.Start();
            await Scan;
        }

        private async Task FindInfo(IPAddress target, string username, string password)
        {
            Task Find = new Task(() =>
            {
                //Find system information using Win32_ComputerSystem class
                try
                {
                    ConnectionOptions connectionOptions = new ConnectionOptions();
                    connectionOptions.Username = username;
                    connectionOptions.Password = password;
                    connectionOptions.Impersonation = ImpersonationLevel.Impersonate;

                    ManagementScope managementScope = new ManagementScope($"\\\\{target}\\root\\CIMV2", connectionOptions);
                    managementScope.Options.EnablePrivileges = true;

                    ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(managementScope, query);
                    ManagementObjectCollection queryCollection = searcher.Get();
                    foreach (ManagementObject managementObject in queryCollection)
                    {
                        logRichTextBox.Invoke(() =>
                        {
                            string[] properties = { "Domain", "Model", "Name", "PrimaryOwnerName", "TotalPhysicalMemory" };
                            foreach (string property in properties)
                            {
                                logRichTextBox.AppendText($"\r\n{property}: {managementObject[property]}\r\n");
                            }
                        });
                        break;
                    }
                }
                catch (Exception ex)
                {
                    logRichTextBox.Invoke(() =>
                    {
                        logRichTextBox.AppendText("\r\nCould not retrieve information\r\n");
                        logRichTextBox.AppendText($"Error: {ex.Message}\r\n");
                    });
                }
            });
            Find.Start();
            await Find;
        }

        private void networkSnifferButton_Click(object sender, EventArgs e)
        {
            NetworkSniffer networkSniffer = new NetworkSniffer();
            networkSniffer.Show();
        }
    }
}
