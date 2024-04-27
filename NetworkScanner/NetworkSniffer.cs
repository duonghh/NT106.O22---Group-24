using NetworkScanner.Packets;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace NetworkScanner
{
    public partial class NetworkSniffer : Form
    {
        public NetworkSniffer()
        {
            InitializeComponent();
        }

        private void NetworkSniffer_Load(object sender, EventArgs e)
        {
            #region Lấy ra tất cả Network Interface khả dụng
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    interfaceComboBox.Items.Add(networkInterface.Name);
            }
            #endregion
        }

        private void interfaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Lấy ra thông tin của Interface khi được chọn
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.Name.Contains(interfaceComboBox.Text))    // Lấy ra Interface được chọn
                {
                    IPInterfaceProperties properties = networkInterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddressTextBox.Text = ip.Address.ToString();
                        }
                    }
                    break;
                }
            }
            #endregion

            startButton.Enabled = true;
        }

        CancellationTokenSource cancellationTokenSource;
        CancellationToken cancellationToken;
        private async void startButton_Click(object sender, EventArgs e)
        {
            interfaceComboBox.Enabled = false;
            startButton.Enabled = false;
            stopButton.Enabled = true;

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            Task task = CapturePackets();
            await task;

            interfaceComboBox.Enabled = true;
            startButton.Enabled = true;
            stopButton.Enabled = false;
            deleteButton.Enabled = true;
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
        }

        List<IPPacket> iPPackets = new List<IPPacket>();
        int numberOfPacket = 0;
        private async Task CapturePackets()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            IPAddress target = System.Net.IPAddress.Parse(ipAddressTextBox.Text);
            socket.Bind(new IPEndPoint(target, 0));
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

            byte[] bytesIn = new byte[4] { 1, 0, 0, 0 };
            byte[] bytesOut = new byte[4];
            socket.IOControl(IOControlCode.ReceiveAll, bytesIn, bytesOut);

            Task Capture = new Task(() =>
            {
                byte[] bytesBuffer = new byte[8192];
                while (!cancellationToken.IsCancellationRequested)
                {
                    int bufferSize = socket.ReceiveBufferSize;
                    int bytesReceived = socket.Receive(bytesBuffer, bytesBuffer.Length, SocketFlags.None);

                    if (bytesReceived > 0)
                    {
                        //getting IP header and data information
                        IPPacket ipPacket = new IPPacket(bytesBuffer, bytesReceived);

                        //searching which uperlevel protocol contain IP packet
                        switch (ipPacket.Protocol)
                        {
                            case "TCP":
                                {
                                    // Increase number of packets counted and put the packet in the Packet List
                                    numberOfPacket++;
                                    iPPackets.Add(ipPacket);

                                    //if IP contains TCP creating new TCPData object
                                    //and assigning all TCP fields
                                    TCPPacket tcpPacket = new TCPPacket(ipPacket.Data, ipPacket.MessageLength);

                                    // Fill the list view control
                                    listView.Invoke(() =>
                                    {
                                        ListViewItem item = new ListViewItem(new string[]
                                        {
                                            numberOfPacket.ToString(),
                                            DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(),
                                            ipPacket.SourceAddress.ToString(),
                                            tcpPacket.SourcePort,
                                            ipPacket.DestinationAddress.ToString(),
                                            tcpPacket.DestinationPort,
                                            ipPacket.Protocol,
                                            ipPacket.TotalLength
                                        });
                                        listView.Items.Add(item);
                                    });
                                }
                                break;

                            case "UDP":
                                {
                                    // Increase number of packets counted and put it in the Packet List
                                    numberOfPacket++;
                                    iPPackets.Add(ipPacket);

                                    //if IP contains TCP creating new TCPData object
                                    //and assigning all TCP fields
                                    UDPPacket udpPacket = new UDPPacket(ipPacket.Data, ipPacket.MessageLength);

                                    // Fill the list view control
                                    listView.Invoke(() =>
                                    {
                                        ListViewItem item = new ListViewItem(new string[]
                                        {
                                            numberOfPacket.ToString(),
                                            DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString(),
                                            ipPacket.SourceAddress.ToString(),
                                            udpPacket.SourcePort,
                                            ipPacket.DestinationAddress.ToString(),
                                            udpPacket.DestinationPort,
                                            ipPacket.Protocol,
                                            ipPacket.TotalLength
                                        });
                                        listView.Items.Add(item);
                                    });
                                }

                                break;
                        }
                    }
                }
            }, cancellationToken);
            Capture.Start();
            await Capture;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            iPPackets.Clear();
            listView.Items.Clear();
            numberOfPacket = 0;
            deleteButton.Enabled = false;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getting the index of selected item
            ListView.SelectedIndexCollection indexCollection = listView.SelectedIndices;
            if (indexCollection.Count > 0 )
            {
                // getting the selected packet based on index
                int index = indexCollection[0];
                IPPacket selectedPacket = iPPackets[index];

                switch (selectedPacket.Protocol)
                {
                    case "TCP":
                        {
                            TCPPacket tcpPacket = new TCPPacket(selectedPacket.Data, selectedPacket.MessageLength);
                            #region Adding information to the Tree View control

                            treeView.Nodes.Clear();

                            TreeNode node = new TreeNode("IP");
                            node.ForeColor = Color.Green;

                            node.Nodes.Add("Protocol version: " + selectedPacket.Version);
                            node.Nodes.Add("Header lenght: " + selectedPacket.HeaderLength);
                            node.Nodes.Add("Type ofservice: " + selectedPacket.TypeOfService);
                            node.Nodes.Add("Total lenght: " + selectedPacket.TotalLength);
                            node.Nodes.Add("Identification No: " + selectedPacket.Identification);
                            node.Nodes.Add("Flags: " + selectedPacket.Flags);
                            node.Nodes.Add("Fragmentation offset: " + selectedPacket.FragmentationOffset);
                            node.Nodes.Add("TTL: " + selectedPacket.TTL);
                            node.Nodes.Add("Checksum: " + selectedPacket.Checksum);
                            node.Nodes.Add(String.Format("Source address: {0}: {1}", selectedPacket.SourceAddress, tcpPacket.SourcePort));
                            node.Nodes.Add(String.Format("Destination address: {0}: {1}", selectedPacket.DestinationAddress, tcpPacket.DestinationPort));

                            TreeNode subNode = new TreeNode("TCP");
                            subNode.ForeColor = Color.Red;

                            subNode.Nodes.Add("Sequence No: " + tcpPacket.SequenceNumber);
                            subNode.Nodes.Add("Acknowledgement NO: " + tcpPacket.AcknowledgementNumber);
                            subNode.Nodes.Add("Header lenght: " + tcpPacket.HeaderLength);
                            subNode.Nodes.Add("Flags: " + tcpPacket.Flags);
                            subNode.Nodes.Add("Window size: " + tcpPacket.WindowSize);
                            subNode.Nodes.Add("Checksum: " + tcpPacket.Checksum);
                            subNode.Nodes.Add("Message lenght: " + tcpPacket.MessageLength);

                            node.Nodes.Add(subNode);
                            treeView.Nodes.Add(node);
                            treeView.ExpandAll();
                            #endregion

                            #region Adding payload context to the Rich Text Box control
                            richTextBox.Clear();
                            byte[] payload = tcpPacket.Data;
                            richTextBox.AppendText(Encoding.Default.GetString(payload)); 
                            #endregion
                        } break;

                    case "UDP":
                        {
                            UDPPacket udpPacket = new UDPPacket(selectedPacket.Data, selectedPacket.MessageLength);
                            #region Adding information to the Tree View control

                            treeView.Nodes.Clear();

                            TreeNode node = new TreeNode("IP");
                            node.ForeColor = Color.Green;

                            node.Nodes.Add("Protocol version: " + selectedPacket.Version);
                            node.Nodes.Add("Header lenght: " + selectedPacket.HeaderLength);
                            node.Nodes.Add("Type ofservice: " + selectedPacket.TypeOfService);
                            node.Nodes.Add("Total lenght: " + selectedPacket.TotalLength);
                            node.Nodes.Add("Identification No: " + selectedPacket.Identification);
                            node.Nodes.Add("Flags: " + selectedPacket.Flags);
                            node.Nodes.Add("Fragmentation offset: " + selectedPacket.FragmentationOffset);
                            node.Nodes.Add("TTL: " + selectedPacket.TTL);
                            node.Nodes.Add("Checksum: " + selectedPacket.Checksum);
                            node.Nodes.Add(String.Format("Source address: {0}: {1}", selectedPacket.SourceAddress, udpPacket.SourcePort));
                            node.Nodes.Add(String.Format("Destination address: {0}: {1}", selectedPacket.DestinationAddress, udpPacket.DestinationPort));

                            TreeNode subNode = new TreeNode("UDP");
                            subNode.ForeColor = Color.Red;

                            subNode.Nodes.Add("Length: " + udpPacket.Length);
                            subNode.Nodes.Add("Checksum: " + udpPacket.Checksum);

                            node.Nodes.Add(subNode);
                            treeView.Nodes.Add(node);
                            treeView.ExpandAll();
                            #endregion

                            #region Adding payload context to the Rich Text Box control
                            richTextBox.Clear();
                            byte[] payload = udpPacket.Data;
                            richTextBox.AppendText(Encoding.Default.GetString(payload)); 
                            #endregion
                        } break;
                }
            }
        }
    }
}
