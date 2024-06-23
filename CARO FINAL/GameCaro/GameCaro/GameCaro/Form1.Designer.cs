namespace GameCaro
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlChessBoard = new Panel();
            panel2 = new Panel();
            pctbImage = new PictureBox();
            panel3 = new Panel();
            prgbCountDown = new ProgressBar();
            label4 = new Label();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnLAN = new Button();
            txbIP = new TextBox();
            pctbMark = new PictureBox();
            txbPlayerName = new TextBox();
            tmCountDown = new System.Windows.Forms.Timer(components);
            menuStrip1 = new MenuStrip();
            mENUToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctbImage).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctbMark).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.Location = new Point(11, 30);
            pnlChessBoard.Margin = new Padding(2);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(652, 614);
            pnlChessBoard.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(pctbImage);
            panel2.Location = new Point(681, 30);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(215, 218);
            panel2.TabIndex = 1;
            // 
            // pctbImage
            // 
            pctbImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pctbImage.BackgroundImage = Properties.Resources.ava;
            pctbImage.BackgroundImageLayout = ImageLayout.Stretch;
            pctbImage.Location = new Point(3, 2);
            pctbImage.Margin = new Padding(2);
            pctbImage.Name = "pctbImage";
            pctbImage.Size = new Size(211, 216);
            pctbImage.TabIndex = 0;
            pctbImage.TabStop = false;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel3.Controls.Add(prgbCountDown);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(btnLAN);
            panel3.Controls.Add(txbIP);
            panel3.Controls.Add(pctbMark);
            panel3.Controls.Add(txbPlayerName);
            panel3.Location = new Point(681, 257);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.Size = new Size(215, 387);
            panel3.TabIndex = 2;
            // 
            // prgbCountDown
            // 
            prgbCountDown.Location = new Point(10, 182);
            prgbCountDown.Margin = new Padding(2);
            prgbCountDown.Name = "prgbCountDown";
            prgbCountDown.Size = new Size(194, 29);
            prgbCountDown.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 161);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(88, 20);
            label4.TabIndex = 8;
            label4.Text = "Countdown:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 48);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(87, 20);
            label5.TabIndex = 9;
            label5.Text = "Next Player:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 219);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(24, 20);
            label3.TabIndex = 7;
            label3.Text = "IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 109);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(96, 20);
            label2.TabIndex = 6;
            label2.Text = "Player Name:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Ink Free", 13.875F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 64, 0);
            label1.Location = new Point(18, 338);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(195, 30);
            label1.TabIndex = 5;
            label1.Text = "5 in a line to win";
            // 
            // btnLAN
            // 
            btnLAN.Location = new Point(55, 283);
            btnLAN.Margin = new Padding(2);
            btnLAN.Name = "btnLAN";
            btnLAN.Size = new Size(92, 29);
            btnLAN.TabIndex = 4;
            btnLAN.Text = "Connect";
            btnLAN.UseVisualStyleBackColor = true;
            btnLAN.Click += btnLAN_Click;
            // 
            // txbIP
            // 
            txbIP.Location = new Point(10, 241);
            txbIP.Margin = new Padding(2);
            txbIP.Name = "txbIP";
            txbIP.Size = new Size(195, 27);
            txbIP.TabIndex = 3;
            txbIP.Text = "127.0.0.1";
            txbIP.TextChanged += txbIP_TextChanged;
            // 
            // pctbMark
            // 
            pctbMark.Location = new Point(109, 19);
            pctbMark.Margin = new Padding(2);
            pctbMark.Name = "pctbMark";
            pctbMark.Size = new Size(95, 83);
            pctbMark.SizeMode = PictureBoxSizeMode.StretchImage;
            pctbMark.TabIndex = 2;
            pctbMark.TabStop = false;
            // 
            // txbPlayerName
            // 
            txbPlayerName.BackColor = Color.RosyBrown;
            txbPlayerName.Location = new Point(10, 131);
            txbPlayerName.Margin = new Padding(2);
            txbPlayerName.Name = "txbPlayerName";
            txbPlayerName.ReadOnly = true;
            txbPlayerName.Size = new Size(195, 27);
            txbPlayerName.TabIndex = 0;
            // 
            // tmCountDown
            // 
            tmCountDown.Tick += tmCountDown_Tick;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mENUToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(925, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // mENUToolStripMenuItem
            // 
            mENUToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, undoToolStripMenuItem, quitToolStripMenuItem });
            mENUToolStripMenuItem.Name = "mENUToolStripMenuItem";
            mENUToolStripMenuItem.Size = new Size(65, 24);
            mENUToolStripMenuItem.Text = "MENU";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(180, 26);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U;
            undoToolStripMenuItem.Size = new Size(180, 26);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            quitToolStripMenuItem.Size = new Size(180, 26);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 657);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(pnlChessBoard);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Game Caro";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Shown += Form1_Shown;
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pctbImage).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctbMark).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlChessBoard;
        private Panel panel2;
        private Panel panel3;
        private PictureBox pctbImage;
        private TextBox txbPlayerName;
        private Button btnLAN;
        private TextBox txbIP;
        private PictureBox pctbMark;
        private ProgressBar prgbCountDown;
        private Label label1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private System.Windows.Forms.Timer tmCountDown;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mENUToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
    }
}
