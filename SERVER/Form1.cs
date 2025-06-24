using System.Net.Sockets;
using System.Net;

namespace SERVER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void StartServer()
        {
            Task.Run(() =>
            {
                Socket s = new Socket(AddressFamily.InterNetwork,
                                      SocketType.Stream,
                                      ProtocolType.Tcp); // ����� ������������ ProtocolType.Tcp
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint ep = new IPEndPoint(ip, 1024);
                try
                {
                    s.Bind(ep);
                    s.Listen(10);
                    AppendLog("������ ������� � ������� ���� 1024...");
                    while (true)
                    {
                        Socket ns = s.Accept();
                        string clientEndPoint = ns.RemoteEndPoint.ToString();
                        AppendLog($"����������� �� {clientEndPoint}");
                        string message = DateTime.Now.ToString();
                        byte[] msgBytes = System.Text.Encoding.ASCII.GetBytes(message);
                        ns.Send(msgBytes);
                        ns.Shutdown(SocketShutdown.Both);
                        ns.Close();
                    }
                }
                catch (SocketException ex)
                {
                    AppendLog($"SocketException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    AppendLog($"����������: {ex.Message}");
                }
                finally
                {
                    s.Close();
                }
            });
        }

        // ����� ��� ����������� ���������� ��������� � UI
        private void AppendLog(string message)
        {
            listBoxLog.Text += "\n" + message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartServer();
        }
    }
}
