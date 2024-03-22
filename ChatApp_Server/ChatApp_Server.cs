using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatApp_Server
{
    public partial class ChatApp_Server : Form
    {
        public ChatApp_Server()
        {
            InitializeComponent();
        }

        TcpListener tcpListener;
        static volatile bool started = false;
        static volatile List<TcpClient> connectedClients = new List<TcpClient>();

        private void startButton_Click(object sender, EventArgs e)
        {
            #region Yêu cầu nhập
            if (portTextBox.Text == string.Empty)
            {
                MessageBox.Show("Port is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region Tạo thread Listening Server
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), int.Parse(portTextBox.Text));
            StartServer();
            #endregion
        }

        private void StartServer()
        {
            try
            {
                tcpListener.Start();

                #region Thông báo đã khởi động server
                chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                {
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Italic);
                    chatScreenRichTextBox.AppendText("Server started on" + tcpListener.LocalEndpoint + Environment.NewLine);
                    chatScreenRichTextBox.AppendText("Waiting for a connection..." + Environment.NewLine);
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                }));
                #endregion

                #region Điều chỉnh khả năng tương tác với giao diện
                startButton.Invoke(new MethodInvoker(delegate
                {
                    startButton.Enabled = false;
                }));
                stopButton.Invoke(new MethodInvoker(delegate
                {
                    stopButton.Enabled = true;
                }));

                sendButton.Invoke(new MethodInvoker(delegate
                {
                    sendButton.Enabled = true;
                }));
                portTextBox.Invoke(new MethodInvoker(delegate
                {
                    portTextBox.Enabled = false;
                }));
                #endregion

                started = true;

                #region Luồng chấp nhận kết nối từ Client
                Thread acceptThread = new Thread(AcceptClientThread);
                acceptThread.IsBackground = true;
                acceptThread.Start();
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                started = false;
                return;
            }
        }

        private void StopServer()
        {
            try
            {
                Thread.Sleep(100);
                started = false;

                string message = "Server đã đóng, tự động ngắt kết nối";
                foreach (TcpClient client in connectedClients)
                {
                    Stream stream = client.GetStream();
                    StreamWriter streamWriter = new StreamWriter(stream);
                    streamWriter.Write(message);
                    streamWriter.Flush();
                }

                tcpListener.Stop();
                tcpListener.Dispose();

                #region Điều chỉnh khả năng tương tác với giao diện
                startButton.Invoke(new MethodInvoker(delegate
                {
                    startButton.Enabled = true;
                }));
                stopButton.Invoke(new MethodInvoker(delegate
                {
                    stopButton.Enabled = false;
                }));

                sendButton.Invoke(new MethodInvoker(delegate
                {
                    sendButton.Enabled = false;
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
                started = false;
                return;
            }
        }

        private void AcceptClientThread()
        {
            try
            {
                while (started)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    connectedClients.Add(tcpClient);

                    #region Thông báo đã có client kết nối
                    chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                    {
                        chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                        chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Italic);
                        chatScreenRichTextBox.AppendText("Đã có client kết nối!" + Environment.NewLine);
                        chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                        chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                    }));
                    #endregion

                    Thread clientThread = new Thread(() =>
                    {
                        HandleClientThread(tcpClient);
                    });
                    clientThread.IsBackground = true;
                    clientThread.Start();

                    if (!started) break;
                }
            } catch (Exception ex)
            {
                #region Thông báo đã đóng server
                chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                {
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Italic);
                    chatScreenRichTextBox.AppendText("Server closed: " + ex.Message + "\r\n");
                    chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                }));
                #endregion
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

        private void HandleClientThread(TcpClient client)
        {
            while (IsConnected(client))
            {
                Thread.Sleep(100);
                Stream stream = client.GetStream();
                byte[] bytes = new byte[1024];
                int count = stream.Read(bytes, 0, bytes.Length);
                string message = Encoding.UTF8.GetString(bytes, 0, count);

                if (message == "Client yêu cầu đóng kết nối")
                {
                    break;
                }
                else 
                {
                    chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                    {
                        chatScreenRichTextBox.AppendText(message);
                    }));

                    foreach (TcpClient anotherClient in connectedClients)
                    {
                        if (client != anotherClient)
                        {
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            }

            #region Thông báo đã có client ngắt kết nối
            chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
            {
                chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Italic);
                chatScreenRichTextBox.AppendText("Client disconnected: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address + "\r\n");
                chatScreenRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                chatScreenRichTextBox.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
            }));
            #endregion

            connectedClients.Remove(client);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (importTextBox.Text != string.Empty)
            {
                string message = string.Empty;
                importTextBox.Invoke(new MethodInvoker(delegate
                {
                    message = "[Server]: " + importTextBox.Text + "\r\n";
                    importTextBox.Clear();
                }));
                foreach (TcpClient client in connectedClients)
                {
                    Stream stream = client.GetStream();
                    byte[] bytes = Encoding.UTF8.GetBytes(message);
                    stream.Write(bytes, 0, bytes.Length);
                }
                chatScreenRichTextBox.Invoke(new MethodInvoker(delegate
                {
                    chatScreenRichTextBox.AppendText(message);
                }));
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopServer();
        }
    }
}
