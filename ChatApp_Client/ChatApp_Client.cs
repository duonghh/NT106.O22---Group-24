using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Windows.Forms.AxHost;

namespace ChatApp_Client
{
    public partial class ChatApp_Client : Form
    {
        public ChatApp_Client()
        {
            InitializeComponent();
        }

        TcpClient tcpClient;
        static volatile bool connected = false;

        private void connectButton_Click(object sender, EventArgs e)
        {
            #region Yêu cầu nhập
            if (usernameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Username is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ipAddressTextBox.Text == string.Empty)
            {
                MessageBox.Show("IP Address is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (portTextBox.Text == string.Empty)
            {
                MessageBox.Show("Port is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region Tạo client Thread
            Thread thread = new Thread(Connect);
            thread.IsBackground = true;
            thread.Start();
            #endregion
        }

        private void Connect()
        {
            try
            {
                tcpClient = new TcpClient();
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddressTextBox.Text), int.Parse(portTextBox.Text));
                tcpClient.Connect(ipEndPoint);

                connected = true;

                #region Thông báo đã kết nối thành công
                chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                {
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Italic);
                    chatScreenRichTextBox.AppendText("Connected" + Environment.NewLine);
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                }));
                #endregion

                #region Điều chỉnh khả năng tương tác với giao diện
                connectButton.Invoke(new MethodInvoker(delegate
                {
                    connectButton.Enabled = false;
                }));
                disconectButton.Invoke(new MethodInvoker(delegate
                {
                    disconectButton.Enabled = true;
                }));
                sendButton.Invoke(new MethodInvoker(delegate
                {
                    sendButton.Enabled = true;
                }));
                usernameTextBox.Invoke(new MethodInvoker(delegate
                {
                    usernameTextBox.Enabled = false;
                }));
                ipAddressTextBox.Invoke(new MethodInvoker(delegate
                {
                    ipAddressTextBox.Enabled = false;
                }));
                portTextBox.Invoke(new MethodInvoker(delegate
                {
                    portTextBox.Enabled = false;
                }));
                #endregion

                while (IsConnected(tcpClient))
                {
                    Thread.Sleep(100);
                    Stream stream = tcpClient.GetStream();
                    byte[] bytes = new byte[1024];
                    int count = stream.Read(bytes, 0, bytes.Length);
                    string message = Encoding.UTF8.GetString(bytes, 0, count);

                    if (message == "Server đã đóng, tự động ngắt kết nối" || message == "Client yêu cầu đóng kết nối")
                    {
                        Disconect();
                        return;
                    }

                    chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                    {
                        chatScreenRichTextBox.AppendText(message);
                    }));

                    if (!IsConnected(tcpClient))
                    {
                        connected = false;
                        break;
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connected = false;
                return;
            }
        }

        static bool IsConnected(TcpClient client)
        {
            try
            {
                if (client != null && client.Client != null && client.Client.Connected)
                {
                    if (client.Client.Poll(0, SelectMode.SelectRead))
                    {
                        byte[] buff = new byte[1];
                        if (client.Client.Receive(buff, SocketFlags.Peek) == 0)
                            return false;
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void Disconect()
        {
            try
            {
                Thread.Sleep(100);

                string message = "Client yêu cầu đóng kết nối";
                Stream stream = tcpClient.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                stream.Write(bytes);

                connected = false;

                tcpClient.Close();
                tcpClient.Dispose();

                #region Thông báo đã ngắt kết nối
                chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                {
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Italic);
                    chatScreenRichTextBox.AppendText("Disconected" + Environment.NewLine);
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                }));
                #endregion

                #region Điều chỉnh khả năng tương tác với giao diện
                connectButton.Invoke(new MethodInvoker(delegate
                {
                    connectButton.Enabled = true;
                }));
                disconectButton.Invoke(new MethodInvoker(delegate
                {
                    disconectButton.Enabled = false;
                }));
                sendButton.Invoke(new MethodInvoker(delegate
                {
                    sendButton.Enabled = false;
                }));
                usernameTextBox.Invoke(new MethodInvoker(delegate
                {
                    usernameTextBox.Enabled = true;
                }));
                ipAddressTextBox.Invoke(new MethodInvoker(delegate
                {
                    ipAddressTextBox.Enabled = true;
                }));
                portTextBox.Invoke(new MethodInvoker(delegate
                {
                    portTextBox.Enabled = true;
                }));
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connected = false;
                return;
            }

        }

        private void disconectButton_Click(object sender, EventArgs e)
        {
            Disconect();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (importTextBox.Text != string.Empty)
            {
                string message = "[" + usernameTextBox.Text + "]: " + importTextBox.Text + "\r\n";
                Stream stream = tcpClient.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                stream.Write(bytes);

                chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                {
                    chatScreenRichTextBox.AppendText(message);
                }));
                importTextBox.Invoke(new MethodInvoker(delegate
                {
                    importTextBox.Clear();
                }));
            }
        }
    }
}
