using System.Net;
using System.Net.Sockets;
using System.Text;

namespace N3
{
    public partial class Form1 : Form
    {
        private Socket listenerSocket;
        private Socket clientSocket;
        private Thread listenThread;

        public Form1()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            this.FormClosing += ServerForm_FormClosing;
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            int port = int.Parse(txtPort.Text);
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            listenerSocket.Listen(1);
            AppendMessage($"Сервер запущен на порту {port}. Ожидание клиента...");
            listenThread = new Thread(AcceptClient);
            listenThread.IsBackground = true;
            listenThread.Start();
            btnStart.Enabled = false;
        }

        private void AcceptClient()
        {
            clientSocket = listenerSocket.Accept();
            AppendMessage($"Клиент подключился: {clientSocket.RemoteEndPoint}");
            ReceiveMessages();
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    int received = clientSocket.Receive(buffer);
                    if (received > 0)
                    {
                        string msg = Encoding.ASCII.GetString(buffer, 0, received);
                        AppendMessage($"Клиент: {msg}");
                        if (msg.Trim() == "Bye")
                        {
                            AppendMessage("Соединение закрыто");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppendMessage($"Ошибка: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        private void CloseConnection()
        {
            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket = null;
            }
            AppendMessage("Клиент отключился");
            Invoke(new Action(() => btnStart.Enabled = true));
        }

        private void AppendMessage(string message)
        {
            if (rtbMessages.InvokeRequired)
            {
                rtbMessages.Invoke(new Action(() => rtbMessages.AppendText(message + Environment.NewLine)));
            }
            else
            {
                rtbMessages.AppendText(message + Environment.NewLine);
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clientSocket != null)
            {
                try
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
                catch { }
            }
            if (listenerSocket != null)
            {
                try
                {
                    listenerSocket.Close();
                }
                catch { }
            }
            if (listenThread != null && listenThread.IsAlive)
            {
                listenThread.Abort();
            }
        }
    }
}
