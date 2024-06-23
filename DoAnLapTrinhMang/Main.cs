namespace DoAnLapTrinhMang
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        int sendfile_client_id = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            SendFile_Client sendFile_Client = new SendFile_Client(sendfile_client_id++);
            sendFile_Client.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chat chat = new Chat();
            chat.Show();
        }
    }
}