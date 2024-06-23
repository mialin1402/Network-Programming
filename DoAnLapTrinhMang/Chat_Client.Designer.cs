namespace DoAnLapTrinhMang
{
    partial class Chat_Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chatMessages = new ListBox();
            rtbMessage = new RichTextBox();
            btnSend = new Button();
            fileList = new ListBox();
            label1 = new Label();
            tbUserName = new TextBox();
            tbServerIP = new TextBox();
            tbServerPort = new TextBox();
            label2 = new Label();
            label3 = new Label();
            cbUsers = new ComboBox();
            label4 = new Label();
            btnConnect = new Button();
            btnSendFile = new Button();
            SuspendLayout();
            // 
            // chatMessages
            // 
            chatMessages.FormattingEnabled = true;
            chatMessages.ItemHeight = 20;
            chatMessages.Location = new Point(12, 86);
            chatMessages.Name = "chatMessages";
            chatMessages.Size = new Size(634, 304);
            chatMessages.TabIndex = 0;
            // 
            // rtbMessage
            // 
            rtbMessage.Location = new Point(51, 398);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.Size = new Size(595, 40);
            rtbMessage.TabIndex = 1;
            rtbMessage.Text = "";
            // 
            // btnSend
            // 
            btnSend.Location = new Point(657, 398);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(70, 40);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // fileList
            // 
            fileList.FormattingEnabled = true;
            fileList.ItemHeight = 20;
            fileList.Location = new Point(657, 86);
            fileList.Name = "fileList";
            fileList.Size = new Size(131, 304);
            fileList.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(82, 20);
            label1.TabIndex = 4;
            label1.Text = "User Name";
            // 
            // tbUserName
            // 
            tbUserName.Location = new Point(12, 32);
            tbUserName.Name = "tbUserName";
            tbUserName.Size = new Size(181, 27);
            tbUserName.TabIndex = 5;
            tbUserName.Text = "Duy";
            // 
            // tbServerIP
            // 
            tbServerIP.Location = new Point(583, 9);
            tbServerIP.Name = "tbServerIP";
            tbServerIP.Size = new Size(125, 27);
            tbServerIP.TabIndex = 6;
            tbServerIP.Text = "127.0.0.1";
            // 
            // tbServerPort
            // 
            tbServerPort.Location = new Point(583, 42);
            tbServerPort.Name = "tbServerPort";
            tbServerPort.Size = new Size(125, 27);
            tbServerPort.TabIndex = 7;
            tbServerPort.Text = "8888";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(527, 9);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 8;
            label2.Text = "Server";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(535, 45);
            label3.Name = "label3";
            label3.Size = new Size(35, 20);
            label3.TabIndex = 9;
            label3.Text = "Port";
            // 
            // cbUsers
            // 
            cbUsers.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUsers.FormattingEnabled = true;
            cbUsers.Location = new Point(199, 32);
            cbUsers.Name = "cbUsers";
            cbUsers.Size = new Size(151, 28);
            cbUsers.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(199, 9);
            label4.Name = "label4";
            label4.Size = new Size(60, 20);
            label4.TabIndex = 11;
            label4.Text = "Send to";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(714, 9);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(74, 60);
            btnConnect.TabIndex = 12;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSendFile
            // 
            btnSendFile.Location = new Point(12, 398);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(33, 40);
            btnSendFile.TabIndex = 13;
            btnSendFile.Text = "...";
            btnSendFile.UseVisualStyleBackColor = true;
            btnSendFile.Click += btnSendFile_Click;
            // 
            // Chat_Client
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSendFile);
            Controls.Add(btnConnect);
            Controls.Add(label4);
            Controls.Add(cbUsers);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(tbServerPort);
            Controls.Add(tbServerIP);
            Controls.Add(tbUserName);
            Controls.Add(label1);
            Controls.Add(fileList);
            Controls.Add(btnSend);
            Controls.Add(rtbMessage);
            Controls.Add(chatMessages);
            Name = "Chat_Client";
            Text = "Chat_Client";
            FormClosing += Chat_Client_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox chatMessages;
        private RichTextBox rtbMessage;
        private Button btnSend;
        private ListBox fileList;
        private Label label1;
        private TextBox tbUserName;
        private TextBox tbServerIP;
        private TextBox tbServerPort;
        private Label label2;
        private Label label3;
        private ComboBox cbUsers;
        private Label label4;
        private Button btnConnect;
        private Button btnSendFile;
    }
}