namespace DoAnLapTrinhMang
{
    partial class Chat_Server
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
            lbUsers = new ListBox();
            btnStart = new Button();
            lbLogs = new ListBox();
            btnClose = new Button();
            SuspendLayout();
            // 
            // lbUsers
            // 
            lbUsers.FormattingEnabled = true;
            lbUsers.ItemHeight = 20;
            lbUsers.Location = new Point(580, 92);
            lbUsers.Name = "lbUsers";
            lbUsers.Size = new Size(208, 344);
            lbUsers.TabIndex = 0;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(580, 12);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(104, 48);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lbLogs
            // 
            lbLogs.FormattingEnabled = true;
            lbLogs.ItemHeight = 20;
            lbLogs.Location = new Point(12, 12);
            lbLogs.Name = "lbLogs";
            lbLogs.Size = new Size(562, 424);
            lbLogs.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(690, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(104, 48);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // Chat_Server
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(lbLogs);
            Controls.Add(btnStart);
            Controls.Add(lbUsers);
            Name = "Chat_Server";
            Text = "Chat_Server";
            FormClosing += Chat_Server_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbUsers;
        private Button btnStart;
        private ListBox lbLogs;
        private Button btnClose;
    }
}