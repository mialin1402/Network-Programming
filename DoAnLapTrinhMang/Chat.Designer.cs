namespace DoAnLapTrinhMang
{
    partial class Chat
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
            btnClient = new Button();
            label1 = new Label();
            label2 = new Label();
            cbNumOfClients = new ComboBox();
            label3 = new Label();
            btnServer = new Button();
            SuspendLayout();
            // 
            // btnClient
            // 
            btnClient.Location = new Point(149, 78);
            btnClient.Name = "btnClient";
            btnClient.Size = new Size(94, 29);
            btnClient.TabIndex = 0;
            btnClient.Text = "Client";
            btnClient.UseVisualStyleBackColor = true;
            btnClient.Click += btnClient_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono NL SemiBold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(59, 9);
            label1.Name = "label1";
            label1.Size = new Size(161, 40);
            label1.TabIndex = 1;
            label1.Text = "Chat App";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 82);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 2;
            label2.Text = "Clients";
            // 
            // cbNumOfClients
            // 
            cbNumOfClients.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNumOfClients.FormattingEnabled = true;
            cbNumOfClients.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            cbNumOfClients.Location = new Point(99, 78);
            cbNumOfClients.Name = "cbNumOfClients";
            cbNumOfClients.Size = new Size(44, 28);
            cbNumOfClients.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 141);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 4;
            label3.Text = "Server";
            // 
            // btnServer
            // 
            btnServer.Location = new Point(107, 137);
            btnServer.Name = "btnServer";
            btnServer.Size = new Size(94, 29);
            btnServer.TabIndex = 5;
            btnServer.Text = "Server";
            btnServer.UseVisualStyleBackColor = true;
            btnServer.Click += btnServer_Click;
            // 
            // Chat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(279, 213);
            Controls.Add(btnServer);
            Controls.Add(label3);
            Controls.Add(cbNumOfClients);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnClient);
            Name = "Chat";
            Text = "Chat";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnClient;
        private Label label1;
        private Label label2;
        private ComboBox cbNumOfClients;
        private Label label3;
        private Button btnServer;
    }
}