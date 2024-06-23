using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnLapTrinhMang
{
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
            cbNumOfClients.SelectedText = "1";
        }

        private bool btnClientClicked = false;
        private bool btnServerClicked = false;

        private void btnClient_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbNumOfClients.SelectedIndex; i++)
            {
                Chat_Client chat_Client = new Chat_Client();
                chat_Client.Show();
            }
            btnClientClicked = true;
            btnClicked_Check();
        }

        private void btnClicked_Check()
        {
            if (btnServerClicked && btnClientClicked)
            {
                this.Dispose();
            }
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            Chat_Server chat_Server = new Chat_Server();
            chat_Server.Show();
            btnServerClicked = true;
            btnClicked_Check();
        }
    }
}
