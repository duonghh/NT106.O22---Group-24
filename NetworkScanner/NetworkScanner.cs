using NetworkScanner.Packets;
using System.Diagnostics;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

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

            #region Tính toán thanh trạng thái
            pingTaskProgressBar.Value = 0;
            pingTaskProgressBar.Maximum = (broadcastAddress[0] - networkAddress[0] + 1) * (broadcastAddress[1] - networkAddress[1] + 1) * (broadcastAddress[2] - networkAddress[2] + 1) * (broadcastAddress[3] - networkAddress[3] + 1); 
            #endregion

            Task pingEachIpAddress = new Task(async () =>
            {
                // Tạo ra danh sách Task ping
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
            #region Kiểm tra có token huỷ hay không
            if (cancellationToken_1.IsCancellationRequested)
            {
                return;
            } 
            #endregion

            // Ping đến IP mục tiêu
            Ping myPing = new Ping();
            PingReply pingReply;
            pingReply = await myPing.SendPingAsync(target);

            //Ping thành công
            if (pingReply.Status == IPStatus.Success)
            {
                listView.Invoke(() =>
                {
                    listView.Items.Add(new ListViewItem(new string[] { target.ToString() }));
                });

            }

            #region Cập nhật thanh quá trình
            int progressMaxValue = 0;
            int progressCurrentValue = 0;
            pingTaskProgressBar.Invoke(() =>
            {
                pingTaskProgressBar.Value += 1;
                progressMaxValue = pingTaskProgressBar.Maximum;
                progressCurrentValue = pingTaskProgressBar.Value;
            });
            #endregion

            #region Cập nhật trạng thái tiến độ
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
            #endregion
        }

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

                    Task Scan = PortScan(target);
                    await Scan;

                    startButton.Enabled = true;
                    stopButton.Enabled = false;
                }
                else if (featureComboBox.Text == "Kiểm tra tốc độ kết nối")
                {
                    Task Check = CheckConnectionSpeed(target);
                    await Check;
                }
                else if (featureComboBox.Text == "Lắng nghe thông điệp")
                {
                    startButton.Enabled = false;
                    stopButton.Enabled = true;

                    Task Scan = ListenMessage(target);
                    await Scan;

                    startButton.Enabled = true;
                    stopButton.Enabled = false;
                }
                else if (featureComboBox.Text == "Mở thư mực File Sharing")
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = $"\\\\{seletedIpAddressTextBox.Text}",
                        FileName = "explorer.exe",
                    };

                    Process.Start(startInfo);
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
            logRichTextBox.AppendText($"\r\n---------- Quét cổng của địa chỉ {seletedIpAddressTextBox.Text} ----------\r\n");

            Dictionary<int, KeyValuePair<string, string>> ListOfCommonPorts = CreatePortList();
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
            logRichTextBox.AppendText($"\r\n========== Quét cổng của địa chỉ {seletedIpAddressTextBox.Text} hoàn tất! ==========\r\n");
        }

        #region Danh sách các cổng phổ biến
        private Dictionary<int, KeyValuePair<string, string>> CreatePortList()
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
            portInfo.Add(8080, new KeyValuePair<string, string>("HTTP", "TCP/UDP"));
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
                    if (result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2)))
                    {
                        logRichTextBox.Invoke(() =>
                        {
                            logRichTextBox.AppendText($"\r\nCổng: {port}\r\nGiao thức: {protocol}\r\nDịch vụ: {service}\r\n");
                        });
                    }
                }
                catch { }
            });
            Scan.Start();
            await Scan;
        }

        private async Task CheckConnectionSpeed(IPAddress target)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = await ping.SendPingAsync(target);
                if (pingReply.Status == IPStatus.Success)
                {
                    logRichTextBox.AppendText($"Ping time: {pingReply.RoundtripTime} ms\r\n");
                }
                else
                {
                    logRichTextBox.AppendText("Ping failed\r\n");
                }
            } catch (Exception ex)
            {
                logRichTextBox.AppendText("Ping Error\r\n");
                logRichTextBox.AppendText($"Error: {ex.Message}\r\n");
            }
        }
        private async Task ListenMessage(IPAddress target)
        {
            SelectInterfaceForm selectInterfaceForm = new SelectInterfaceForm();
            selectInterfaceForm.ShowDialog();
            if (selectInterfaceForm.selectedIPAddress == null)
            {
                return;
            }

            logRichTextBox.AppendText($"\r\n---------- Đang lắng nghe từ địa chỉ {seletedIpAddressTextBox.Text} ----------\r\n");

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            socket.Bind(new IPEndPoint(selectInterfaceForm.selectedIPAddress, 0));
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

            byte[] bytesIn = new byte[4] { 1, 0, 0, 0 };
            byte[] bytesOut = new byte[4];
            socket.IOControl(IOControlCode.ReceiveAll, bytesIn, bytesOut);

            Task Listen = new Task(() =>
            {
                while(!cancellationToken_2.IsCancellationRequested)
                {
                    int bufferSize = socket.ReceiveBufferSize;
                    byte[] bytesBuffer = new byte[bufferSize];
                    int bytesReceived = socket.Receive(bytesBuffer, bytesBuffer.Length, SocketFlags.None);

                    if (bytesReceived > 0)
                    {
                        //getting IP header and data information
                        IPPacket ipPacket = new IPPacket(bytesBuffer, bytesReceived);
                        if (ipPacket.SourceAddress.Equals(target))
                        {
                            byte[] message = new byte[8192];
                            if (ipPacket.Protocol == "TCP")
                            {
                                TCPPacket tcpPacket = new TCPPacket(ipPacket.Data, ipPacket.MessageLength);
                                message = tcpPacket.Data;
                            }
                            else if (ipPacket.Protocol == "UDP")
                            {
                                UDPPacket udpPacket = new UDPPacket(ipPacket.Data, ipPacket.MessageLength);
                                message= udpPacket.Data;
                            }
                            else
                            {
                                message = ipPacket.Data;
                            }
                            
                            logRichTextBox.Invoke(() =>
                            {
                                logRichTextBox.AppendText($"\r\nTừ {target}: {Encoding.UTF8.GetString(message)}\r\n");
                            });
                        }
                    }
                }

            }, cancellationToken_2);
            Listen.Start();
            await Listen;
            logRichTextBox.AppendText($"\r\n========== Kết thúc lắng nghe! ==========\r\n");
        }

        private void networkSnifferButton_Click(object sender, EventArgs e)
        {
            NetworkSniffer networkSniffer = new NetworkSniffer();
            networkSniffer.Show();
        }
    }
}
