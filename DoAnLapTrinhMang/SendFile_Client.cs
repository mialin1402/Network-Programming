namespace DoAnLapTrinhMang
{
    public partial class SendFile_Client : Form
    {
        public SendFile_Client(int client_id)
        {
            InitializeComponent();
            this.Text = $"Client{client_id}";
            listener = new SendFile_Listener();
            listener.Accepted += listener_Accepted;
            tmrOverallProg = new System.Windows.Forms.Timer();
            tmrOverallProg.Interval = 100;
            tmrOverallProg.Tick += tmrOverallProg_Tick;

            outputFolder = $"Transfers{client_id}";

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            btnConnect.Click += new EventHandler(btnConnect_Click);
            btnStartServer.Click += new EventHandler(btnStartServer_Click);
            btnStopServer.Click += new EventHandler(btnStopServer_Click);
            btnSendFile.Click += new EventHandler(btnSendFile_Click);
            btnPauseTransfer.Click += new EventHandler(btnPauseTransfer_Click);
            btnStopTransfer.Click += new EventHandler(btnStopTransfer_Click);
            btnOpenDir.Click += new EventHandler(btnOpenDir_Click);
            btnClearComplete.Click += new EventHandler(btnClearComplete_Click);

            btnStopServer.Enabled = false;
        }

        private SendFile_Listener listener;
        private SendFile_Transfer transferClient;
        private string outputFolder;
        private System.Windows.Forms.Timer tmrOverallProg;
        private bool serverRunning;

        void tmrOverallProg_Tick(object sender, EventArgs e)
        {
            if (transferClient == null)
                return;
            progressOverall.Value = transferClient.GetOverallProgress();
        }

        void listener_Accepted(object sender, SocketAcceptedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new SocketAcceptedHandler(listener_Accepted), sender, e);
                return;
            }

            listener.Stop();

            transferClient = new SendFile_Transfer(e.Accepted);
            transferClient.OutputFolder = outputFolder;
            registerEvents();
            transferClient.Run();
            tmrOverallProg.Start();
            setConnectionStatus(transferClient.EndPoint.Address.ToString());
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (transferClient == null)
            {
                transferClient = new SendFile_Transfer();
                transferClient.Connect(txtCntHost.Text.Trim(), int.Parse(txtCntPort.Text.Trim()), connectCallback);
                Enabled = false;
            }
            else
            {
                transferClient.Close();
                transferClient = null;
            }
        }

        private void connectCallback(object sender, string error)
        {
            if (InvokeRequired)
            {
                Invoke(new ConnectCallback(connectCallback), sender, error);
                return;
            }
            Enabled = true;
            if (error != null)
            {
                transferClient.Close();
                transferClient = null;
                MessageBox.Show(error, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            registerEvents();
            transferClient.OutputFolder = outputFolder;
            transferClient.Run();
            setConnectionStatus(transferClient.EndPoint.Address.ToString());
            tmrOverallProg.Start();
            btnConnect.Text = "Disconnect";
        }

        private void registerEvents()
        {
            transferClient.Complete += transferClient_Complete;
            transferClient.Disconnected += transferClient_Disconnected;
            transferClient.ProgressChanged += transferClient_ProgressChanged;
            transferClient.Queued += transferClient_Queued;
            transferClient.Stopped += transferClient_Stopped;
        }

        void transferClient_Stopped(object sender, SendFile_Queue queue)
        {
            if (InvokeRequired)
            {
                Invoke(new TransferEventHandler(transferClient_Stopped), sender, queue);
                return;
            }
            lstTransfers.Items[queue.ID.ToString()].Remove();
        }

        void transferClient_Queued(object sender, SendFile_Queue queue)
        {
            if (InvokeRequired)
            {
                Invoke(new TransferEventHandler(transferClient_Queued), sender, queue);
                return;
            }

            ListViewItem i = new ListViewItem();
            i.Text = queue.ID.ToString();
            i.SubItems.Add(queue.Filename);
            i.SubItems.Add(queue.Type == QueueType.Download ? "Download" : "Upload");
            i.SubItems.Add("0%");
            i.SubItems.Add(queue.FS.Length.ToString());
            i.Tag = queue;
            i.Name = queue.ID.ToString();
            lstTransfers.Items.Add(i);
            i.EnsureVisible();

            if (queue.Type == QueueType.Download)
            {
                transferClient.StartTransfer(queue);
            }
        }

        void transferClient_ProgressChanged(object sender, SendFile_Queue queue)
        {
            if (InvokeRequired)
            {
                Invoke(new TransferEventHandler(transferClient_ProgressChanged), sender, queue);
                return;
            }

            lstTransfers.Items[queue.ID.ToString()].SubItems[3].Text = queue.Progress + "%";
        }

        void transferClient_Disconnected(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(transferClient_Disconnected), sender, e);
                return;
            }

            deregisterEvents();

            foreach (ListViewItem item in lstTransfers.Items)
            {
                SendFile_Queue queue = (SendFile_Queue)item.Tag;
                queue.Close();
            }
            lstTransfers.Items.Clear();
            progressOverall.Value = 0;

            transferClient = null;

            setConnectionStatus("-");

            if (serverRunning)
            {
                listener.Start(int.Parse(txtServerPort.Text.Trim()));
                setConnectionStatus("Waiting...");
            }
            else
            {
                btnConnect.Text = "Connect";
            }
        }

        void transferClient_Complete(object sender, SendFile_Queue queue)
        {
            System.Media.SystemSounds.Asterisk.Play();
        }

        private void deregisterEvents()
        {
            if (transferClient == null)
                return;
            transferClient.Complete -= transferClient_Complete;
            transferClient.Disconnected -= transferClient_Disconnected;
            transferClient.ProgressChanged -= transferClient_ProgressChanged;
            transferClient.Queued -= transferClient_Queued;
            transferClient.Stopped -= transferClient_Stopped;
        }

        private void setConnectionStatus(string connectedTo)
        {
            lblConnected.Text = "Connection: " + connectedTo;
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (serverRunning) return;

            serverRunning = true;

            try
            {
                listener.Start(int.Parse(txtServerPort.Text.Trim()));
                setConnectionStatus("Waiting...");
                btnStartServer.Enabled = false;
                btnStopServer.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Unable to listen on port " + txtServerPort.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            if (!serverRunning)
                return;
            if (transferClient != null)
            {
                transferClient.Close();
                transferClient = null;
            }
            listener.Stop();
            tmrOverallProg.Stop();
            setConnectionStatus("-");
            serverRunning = false;
            btnStartServer.Enabled = true;
            btnStopServer.Enabled = false;
        }

        private void btnClearComplete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in lstTransfers.Items)
            {
                SendFile_Queue queue = (SendFile_Queue)i.Tag;

                if (queue.Progress == 100 || !queue.Running)
                {
                    i.Remove();
                }
            }
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fb = new FolderBrowserDialog())
            {
                if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outputFolder = fb.SelectedPath;

                    if (transferClient != null)
                    {
                        transferClient.OutputFolder = outputFolder;
                    }

                    txtSaveDir.Text = outputFolder;
                }
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (transferClient == null)
                return;
            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.Filter = "All Files (*.*)|*.*";
                o.Multiselect = true;

                if (o.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in o.FileNames)
                    {
                        transferClient.QueueTransfer(file);
                    }
                }
            }
        }

        private void btnPauseTransfer_Click(object sender, EventArgs e)
        {
            if (transferClient == null)
                return;
            foreach (ListViewItem i in lstTransfers.SelectedItems)
            {
                SendFile_Queue queue = (SendFile_Queue)i.Tag;
                queue.Client.PauseTransfer(queue);
            }
        }

        private void btnStopTransfer_Click(object sender, EventArgs e)
        {
            if (transferClient == null)
                return;

            foreach (ListViewItem i in lstTransfers.SelectedItems)
            {
                SendFile_Queue queue = (SendFile_Queue)i.Tag;
                queue.Client.StopTransfer(queue);
                i.Remove();
            }

            progressOverall.Value = 0;
        }
    }
}
