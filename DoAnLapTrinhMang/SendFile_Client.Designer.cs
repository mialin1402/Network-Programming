using System.Windows.Forms;

namespace DoAnLapTrinhMang;

partial class SendFile_Client
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
        components = new System.ComponentModel.Container();
        toolStrip1 = new ToolStrip();
        toolStripSplitButton1 = new ToolStripSplitButton();
        portToolStripMenuItem = new ToolStripMenuItem();
        txtServerPort = new ToolStripTextBox();
        toolStripMenuItem2 = new ToolStripSeparator();
        btnStartServer = new ToolStripMenuItem();
        btnStopServer = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        toolStripLabel1 = new ToolStripLabel();
        txtCntHost = new ToolStripTextBox();
        txtCntPort = new ToolStripTextBox();
        btnConnect = new ToolStripButton();
        toolStripSeparator2 = new ToolStripSeparator();
        toolStripSplitButton2 = new ToolStripSplitButton();
        txtSaveDir = new ToolStripTextBox();
        btnOpenDir = new ToolStripMenuItem();
        lstTransfers = new ListView();
        columnHeader1 = new ColumnHeader();
        columnHeader5 = new ColumnHeader();
        columnHeader6 = new ColumnHeader();
        columnHeader7 = new ColumnHeader();
        columnHeader2 = new ColumnHeader();
        menuTransfers = new ContextMenuStrip(components);
        btnSendFile = new ToolStripMenuItem();
        btnStopTransfer = new ToolStripMenuItem();
        btnPauseTransfer = new ToolStripMenuItem();
        toolStripMenuItem1 = new ToolStripSeparator();
        btnClearComplete = new ToolStripMenuItem();
        statusStrip1 = new StatusStrip();
        lblConnected = new ToolStripStatusLabel();
        toolStripStatusLabel2 = new ToolStripStatusLabel();
        toolStripStatusLabel3 = new ToolStripStatusLabel();
        progressOverall = new ToolStripProgressBar();
        toolStrip1.SuspendLayout();
        menuTransfers.SuspendLayout();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // toolStrip1
        // 
        toolStrip1.ImageScalingSize = new Size(20, 20);
        toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripSplitButton1, toolStripSeparator1, toolStripLabel1, txtCntHost, txtCntPort, btnConnect, toolStripSeparator2, toolStripSplitButton2 });
        toolStrip1.Location = new Point(0, 0);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(608, 27);
        toolStrip1.TabIndex = 1;
        toolStrip1.Text = "toolStrip1";
        // 
        // toolStripSplitButton1
        // 
        toolStripSplitButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
        toolStripSplitButton1.DropDownItems.AddRange(new ToolStripItem[] { portToolStripMenuItem, toolStripMenuItem2, btnStartServer, btnStopServer });
        toolStripSplitButton1.ImageTransparentColor = Color.Magenta;
        toolStripSplitButton1.Name = "toolStripSplitButton1";
        toolStripSplitButton1.Size = new Size(69, 24);
        toolStripSplitButton1.Text = "Server";
        // 
        // portToolStripMenuItem
        // 
        portToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { txtServerPort });
        portToolStripMenuItem.Name = "portToolStripMenuItem";
        portToolStripMenuItem.Size = new Size(123, 26);
        portToolStripMenuItem.Text = "Port";
        // 
        // txtServerPort
        // 
        txtServerPort.Name = "txtServerPort";
        txtServerPort.Size = new Size(100, 27);
        txtServerPort.Text = "100";
        // 
        // toolStripMenuItem2
        // 
        toolStripMenuItem2.Name = "toolStripMenuItem2";
        toolStripMenuItem2.Size = new Size(120, 6);
        // 
        // btnStartServer
        // 
        btnStartServer.Name = "btnStartServer";
        btnStartServer.Size = new Size(123, 26);
        btnStartServer.Text = "Start";
        // 
        // btnStopServer
        // 
        btnStopServer.Name = "btnStopServer";
        btnStopServer.Size = new Size(123, 26);
        btnStopServer.Text = "Stop";
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(6, 27);
        // 
        // toolStripLabel1
        // 
        toolStripLabel1.Name = "toolStripLabel1";
        toolStripLabel1.Size = new Size(43, 24);
        toolStripLabel1.Text = "Host:";
        // 
        // txtCntHost
        // 
        txtCntHost.BorderStyle = BorderStyle.FixedSingle;
        txtCntHost.Name = "txtCntHost";
        txtCntHost.Size = new Size(133, 27);
        txtCntHost.Text = "localhost";
        // 
        // txtCntPort
        // 
        txtCntPort.BorderStyle = BorderStyle.FixedSingle;
        txtCntPort.Name = "txtCntPort";
        txtCntPort.Size = new Size(39, 27);
        txtCntPort.Text = "100";
        txtCntPort.TextBoxTextAlign = HorizontalAlignment.Center;
        // 
        // btnConnect
        // 
        btnConnect.DisplayStyle = ToolStripItemDisplayStyle.Text;
        btnConnect.ImageTransparentColor = Color.Magenta;
        btnConnect.Name = "btnConnect";
        btnConnect.Size = new Size(67, 24);
        btnConnect.Text = "Connect";
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(6, 27);
        // 
        // toolStripSplitButton2
        // 
        toolStripSplitButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
        toolStripSplitButton2.DropDownItems.AddRange(new ToolStripItem[] { txtSaveDir, btnOpenDir });
        toolStripSplitButton2.ImageTransparentColor = Color.Magenta;
        toolStripSplitButton2.Name = "toolStripSplitButton2";
        toolStripSplitButton2.Size = new Size(124, 24);
        toolStripSplitButton2.Text = "Save Directory";
        // 
        // txtSaveDir
        // 
        txtSaveDir.Name = "txtSaveDir";
        txtSaveDir.Size = new Size(300, 27);
        txtSaveDir.Text = "...\\Transfers";
        // 
        // btnOpenDir
        // 
        btnOpenDir.Name = "btnOpenDir";
        btnOpenDir.Size = new Size(374, 26);
        btnOpenDir.Text = "Choose...";
        // 
        // lstTransfers
        // 
        lstTransfers.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader5, columnHeader6, columnHeader7, columnHeader2 });
        lstTransfers.ContextMenuStrip = menuTransfers;
        lstTransfers.FullRowSelect = true;
        lstTransfers.Location = new Point(16, 43);
        lstTransfers.Margin = new Padding(4, 5, 4, 5);
        lstTransfers.Name = "lstTransfers";
        lstTransfers.Size = new Size(575, 201);
        lstTransfers.TabIndex = 2;
        lstTransfers.UseCompatibleStateImageBehavior = false;
        lstTransfers.View = View.Details;
        // 
        // columnHeader1
        // 
        columnHeader1.Text = "ID";
        columnHeader1.Width = 79;
        // 
        // columnHeader5
        // 
        columnHeader5.Text = "Filename";
        columnHeader5.Width = 171;
        // 
        // columnHeader6
        // 
        columnHeader6.Text = "Type";
        columnHeader6.TextAlign = HorizontalAlignment.Center;
        columnHeader6.Width = 72;
        // 
        // columnHeader7
        // 
        columnHeader7.DisplayIndex = 4;
        columnHeader7.Text = "Progress";
        columnHeader7.TextAlign = HorizontalAlignment.Right;
        columnHeader7.Width = 68;
        // 
        // columnHeader2
        // 
        columnHeader2.DisplayIndex = 3;
        columnHeader2.Text = "Size";
        columnHeader2.Width = 90;
        // 
        // menuTransfers
        // 
        menuTransfers.ImageScalingSize = new Size(20, 20);
        menuTransfers.Items.AddRange(new ToolStripItem[] { btnSendFile, btnStopTransfer, btnPauseTransfer, toolStripMenuItem1, btnClearComplete });
        menuTransfers.Name = "contextMenuStrip1";
        menuTransfers.Size = new Size(182, 106);
        // 
        // btnSendFile
        // 
        btnSendFile.Name = "btnSendFile";
        btnSendFile.Size = new Size(181, 24);
        btnSendFile.Text = "Send";
        // 
        // btnStopTransfer
        // 
        btnStopTransfer.Name = "btnStopTransfer";
        btnStopTransfer.Size = new Size(181, 24);
        btnStopTransfer.Text = "Cancel";
        // 
        // btnPauseTransfer
        // 
        btnPauseTransfer.Name = "btnPauseTransfer";
        btnPauseTransfer.Size = new Size(181, 24);
        btnPauseTransfer.Text = "Pause";
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(178, 6);
        // 
        // btnClearComplete
        // 
        btnClearComplete.Name = "btnClearComplete";
        btnClearComplete.Size = new Size(181, 24);
        btnClearComplete.Text = "Clear Complete";
        // 
        // statusStrip1
        // 
        statusStrip1.ImageScalingSize = new Size(20, 20);
        statusStrip1.Items.AddRange(new ToolStripItem[] { lblConnected, toolStripStatusLabel2, toolStripStatusLabel3, progressOverall });
        statusStrip1.Location = new Point(0, 258);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Padding = new Padding(1, 0, 19, 0);
        statusStrip1.Size = new Size(608, 30);
        statusStrip1.TabIndex = 3;
        statusStrip1.Text = "statusStrip1";
        // 
        // lblConnected
        // 
        lblConnected.Name = "lblConnected";
        lblConnected.Size = new Size(324, 24);
        lblConnected.Spring = true;
        lblConnected.Text = "Connection: -";
        // 
        // toolStripStatusLabel2
        // 
        toolStripStatusLabel2.Name = "toolStripStatusLabel2";
        toolStripStatusLabel2.Size = new Size(13, 24);
        toolStripStatusLabel2.Text = "|";
        // 
        // toolStripStatusLabel3
        // 
        toolStripStatusLabel3.Name = "toolStripStatusLabel3";
        toolStripStatusLabel3.Size = new Size(116, 24);
        toolStripStatusLabel3.Text = "Overall Progress";
        // 
        // progressOverall
        // 
        progressOverall.Name = "progressOverall";
        progressOverall.Size = new Size(133, 22);
        progressOverall.Style = ProgressBarStyle.Continuous;
        // 
        // SendFile_Client
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(608, 288);
        Controls.Add(statusStrip1);
        Controls.Add(lstTransfers);
        Controls.Add(toolStrip1);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        Name = "SendFile_Client";
        Text = "File Transfer";
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        menuTransfers.ResumeLayout(false);
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ToolStrip toolStrip1;
    private ToolStripSplitButton toolStripSplitButton1;
    private ToolStripMenuItem btnStartServer;
    private ToolStripMenuItem btnStopServer;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripLabel toolStripLabel1;
    private ToolStripTextBox txtCntHost;
    private ToolStripTextBox txtCntPort;
    private ToolStripButton btnConnect;
    private ToolStripSeparator toolStripSeparator2;
    private ListView lstTransfers;
    private ColumnHeader columnHeader1;
    private ContextMenuStrip menuTransfers;
    private ToolStripMenuItem btnSendFile;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem btnClearComplete;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel lblConnected;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private ToolStripStatusLabel toolStripStatusLabel3;
    private ToolStripProgressBar progressOverall;
    private ToolStripMenuItem btnStopTransfer;
    private ToolStripMenuItem btnPauseTransfer;
    private ToolStripSplitButton toolStripSplitButton2;
    private ToolStripTextBox txtSaveDir;
    private ToolStripMenuItem btnOpenDir;
    private ToolStripMenuItem portToolStripMenuItem;
    private ToolStripTextBox txtServerPort;
    private ToolStripSeparator toolStripMenuItem2;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader7;
    private ColumnHeader columnHeader2;
}