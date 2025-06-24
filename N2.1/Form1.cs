namespace N2._1
{
    public partial class Form1 : Form
    {
        private AsyncServer server;
        public Form1()
        {
            InitializeComponent();

            server = new AsyncServer("127.0.0.1", 1024);
            server.OnLog += LogMessage;
        }


        private void LogMessage(string message)
        {

            listBoxLog.Text += "\n" + message;

        }

        private void listBoxLog_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            
            Task.Run(() => server.StartServer());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
        }
    }
}
