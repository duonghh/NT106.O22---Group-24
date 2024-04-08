using System;
using System.Management;
using System.Net;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime;
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

            scanButton.Enabled = true;  // Thay đổi trạng thái của nút Quét
        }

        Task Scan;
        CancellationTokenSource cancellationTokenSource;
        private void scanButton_Click(object sender, EventArgs e)
        {
            scanButton.Enabled = false;
            stopButton.Enabled = true;
            listView.Items.Clear();

            cancellationTokenSource = new CancellationTokenSource();
            CaculateWildcardMask_and_Scan(cancellationTokenSource.Token);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            scanButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private async void CaculateWildcardMask_and_Scan(CancellationToken cancellationToken)
        {
            string subnetMask = subnetMaskTextBox.Text; // Subnet mask cần tính wildcard mask
            IPAddress subnetMaskIP = System.Net.IPAddress.Parse(subnetMask);

            // Chuyển đổi subnet mask thành mảng byte
            byte[] subnetBytes = subnetMaskIP.GetAddressBytes();

            // Tính toán wildcard mask bằng cách đảo ngược các bit trong subnet mask
            byte[] wildcardBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                wildcardBytes[i] = (byte)~subnetBytes[i];
            }

            // Chuyển đổi wildcard mask từ mảng byte thành chuỗi IP
            IPAddress wildcardMask = new IPAddress(wildcardBytes);
            string[] wildcardMaskOctets = wildcardMask.ToString().Split('.');
            if (int.Parse(wildcardMaskOctets[1]) > 0)
            {
                progressBar.Value = 0;
                progressBar.Maximum = int.Parse(wildcardMaskOctets[1]) * 255 * 255;
                Scan = SecondOctetScan(System.Net.IPAddress.Parse(networkTextBox.Text), int.Parse(wildcardMaskOctets[1]), cancellationToken);
                statusLabel.Text = "Quét hoàn tất!";
            }
            else if (int.Parse(wildcardMaskOctets[2]) > 0)
            {
                progressBar.Value = 0;
                progressBar.Maximum = int.Parse(wildcardMaskOctets[2]) * 255;
                Scan = ThirdOctetScan(System.Net.IPAddress.Parse(networkTextBox.Text), int.Parse(wildcardMaskOctets[2]), cancellationToken);
                statusLabel.Text = "Quét hoàn tất!";
            }
            else if (int.Parse(wildcardMaskOctets[3]) > 0)
            {
                progressBar.Value = 0;
                progressBar.Maximum = int.Parse(wildcardMaskOctets[3]);
                Scan = FourthOctetScan(System.Net.IPAddress.Parse(networkTextBox.Text), int.Parse(wildcardMaskOctets[3]), cancellationToken);
                statusLabel.Text = "Quét hoàn tất!";
            }
        }

        private async Task SecondOctetScan(IPAddress networkAddress, int wildcardOctet, CancellationToken cancellationToken)
        {
            string[] ipOctets = networkAddress.ToString().Split(',');
            Task pingEachIpAddress = new Task(() =>
            {
                for (int i = 1; i <= wildcardOctet; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        statusLabel.Invoke(() =>
                        {
                            statusLabel.Text = "Đã dừng việc quét";
                        });
                        break;
                    }

                    string ipString = $"{ipOctets[0]}.{i.ToString()}.{ipOctets[2]}.{ipOctets[3]}";
                    IPAddress ip = System.Net.IPAddress.Parse(ipString);
                    ThirdOctetScan(ip, 255, cancellationToken);
                }
            }, cancellationToken);
            pingEachIpAddress.Start();
            await pingEachIpAddress;
        }

        private async Task ThirdOctetScan(IPAddress networkAddress, int wildcardOctet, CancellationToken cancellationToken)
        {
            string[] ipOctets = networkAddress.ToString().Split('.');
            Task pingEachIpAddress = new Task(() =>
            {
                for (int i = 1; i <= wildcardOctet; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        statusLabel.Invoke(() =>
                        {
                            statusLabel.Text = "Đã dừng việc quét";
                        });
                        break;
                    }

                    string ipString = $"{ipOctets[0]}.{ipOctets[1]}.{i.ToString()}.{ipOctets[3]}";
                    IPAddress ip = System.Net.IPAddress.Parse(ipString);
                    FourthOctetScan(ip, 255, cancellationToken);
                }
            }, cancellationToken);
            pingEachIpAddress.Start();
            await pingEachIpAddress;
        }

        private async Task FourthOctetScan(IPAddress networkAddress, int wildcardOctet, CancellationToken cancellationToken)
        {
            string[] ipOctets = networkAddress.ToString().Split('.');

            Task pingEachIpAddress = new Task(() =>
            {
                for (int i = 1; i <= wildcardOctet; i++)
                {
                    // Kiểm tra xem người dùng có bấm nút dừng hay không
                    if (cancellationToken.IsCancellationRequested)
                    {
                        statusLabel.Invoke(() =>
                        {
                            statusLabel.Text = "Đã dừng việc quét";
                        });
                        break;
                    }

                    string ipString = $"{ipOctets[0]}.{ipOctets[1]}.{ipOctets[2]}.{i.ToString()}";
                    IPAddress ipAddress = System.Net.IPAddress.Parse(ipString);

                    // Đặt thông báo trạng thái
                    statusLabel.Invoke(() =>
                    {
                        statusLabel.Text = $"Đang chờ phản hồi từ địa chỉ IP {ipString}";
                    });
                    // Cập nhật thanh quá trình
                    progressBar.Invoke(() =>
                    {
                        progressBar.Value += 1;
                    });

                    // Ping đến địa chỉ IP
                    Ping myPing = new Ping();
                    PingReply pingReply;
                    try
                    {
                        pingReply = myPing.Send(ipAddress, 200);    // Timeout 0.2s
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                    //Kiểm tra trạng thái gói PingReply
                    if (pingReply.Status == IPStatus.Success)
                    {
                        try
                        {
                            listView.Invoke(() =>
                            {
                                listView.Items.Add(new ListViewItem(new string[] { ipString, "Up" }));
                            });
                        }
                        catch
                        {
                            listView.Invoke(() =>
                            {
                                listView.Items.Add(new ListViewItem(new string[] { ipString, "Up" }));
                            });

                        }
                    }
                    else
                    {
                        listView.Invoke(() =>
                        {
                            listView.Items.Add(new ListViewItem(new string[] { ipString, "Down" }));
                        });
                    }
                }
            }, cancellationToken);
            pingEachIpAddress.Start();
            await pingEachIpAddress;
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            featurePanel.Enabled = true;
        }

        private void getInfoButton_Click(object sender, EventArgs e)
        {

        }

        private void scanPortButton_Click(object sender, EventArgs e)
        {

        }
    }
}
