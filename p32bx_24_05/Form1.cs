using System.Net.Sockets;
using System.Net;
using System.Text;

namespace p32bx_24_05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private async System.Threading.Tasks.Task ConnectToServerAsync()
        {
            // Очистка вывод
            textBoxOutput.Clear();

            IPAddress ip = IPAddress.Parse("207.46.197.32");
            IPEndPoint ep = new IPEndPoint(ip, 80);
            Socket s = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.IP);

            try
            {
                await s.ConnectAsync(ep);
                if (s.Connected)
                {
                    string strSend = "GET\r\n\r\n";
                    byte[] sendBuffer = Encoding.ASCII.GetBytes(strSend);
                    await s.SendAsync(new ArraySegment<byte>(sendBuffer), SocketFlags.None);

                    byte[] buffer = new byte[1024];
                    int l;

                    do
                    {
                        l = await s.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                        string responsePart = Encoding.ASCII.GetString(buffer, 0, l);
                        // Обновляем UI через Invoke, если нужно
                        AppendText(responsePart);
                    } while (l > 0);
                }
                else
                {
                    MessageBox.Show("Ошибка соединения");
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (s.Connected)
                {
                    s.Shutdown(SocketShutdown.Both);
                }
                s.Close();
            }
        }

        private void AppendText(string text)
        {
            if (textBoxOutput.InvokeRequired)
            {
                textBoxOutput.Invoke(new Action<string>(AppendText), text);
            }
            else
            {
                textBoxOutput.AppendText(text);
            }
        }

        private async void buttonConnect_Click_1(object sender, EventArgs e)
        {
            await ConnectToServerAsync();
        }
    }
}
