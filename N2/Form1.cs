namespace N2
{
    public partial class Form1 : Form
    {
        private Server server;
        public Form1()
        {
            InitializeComponent();
            server = new Server("127.0.0.1", 1024, LogMessage);
        }
        private void LogMessage(string message)
        {
            listBoxLog.Text += "\n" + message;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
        }
    }
}
