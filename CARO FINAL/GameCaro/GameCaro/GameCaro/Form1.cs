using System.Net.NetworkInformation;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManager ChessBoard;

        // tao socket
        SocketManager socket;
        #endregion
        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false; // 2 luong dung nhau cung k bao loi

            // tao chessboard 
            ChessBoard = new ChessBoardManager(pnlChessBoard, txbPlayerName, pctbMark);
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            prgbCountDown.Step = Cons.COUNT_DOWN_STEP;
            prgbCountDown.Maximum = Cons.COUNT_DOWN_TIME;
            prgbCountDown.Value = 0;

            // them thuoc tinh cho timer
            tmCountDown.Interval = Cons.COUNT_DOWN_INTERVAL;

            // khoi tao socket
            socket = new SocketManager();

            NewGame();
        }

        #region Methods

        // tao ham EndGame
        void EndGame()
        {
            tmCountDown.Stop();
            pnlChessBoard.Enabled = false; // endgame la k cho danh tiep nua
            undoToolStripMenuItem.Enabled = false;
            //MessageBox.Show("Kết thúc");
        }

        // ham NewGame
        void NewGame()
        {
            prgbCountDown.Value = 0; // reset lai progress
            tmCountDown.Stop(); // tm reset tu dau, cho nguoi choi danh co moi chay
            undoToolStripMenuItem.Enabled = true; // moi lan new game cho phep mo undo
            ChessBoard.DrawChessBoard(); // ve moi ban co
        }

        // ham Quit
        void Quit()
        {
            Application.Exit();
        }

        // ham Undo (luu cac buoc di vao stack)
        void Undo()
        {
            ChessBoard.Undo();
            prgbCountDown.Value = 0;
        }

        void ChessBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCountDown.Start();
            pnlChessBoard.Enabled = false;
            prgbCountDown.Value = 0;

            // gui data buoc di, ep kieu int vi enum la kieu liet ke
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));

            // ai dang danh moi duoc undo
            undoToolStripMenuItem.Enabled = false;

            Listen(); // sv danh cli nghe, cli danh sv nghe lai
        }

        // ham ket thuc gameCaro
        void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();
            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
        }

        private void btnLAN_Click(object sender, EventArgs e)
        {
            socket.IP = txbIP.Text;

            if (!socket.ConnectServer())
            {
                socket.isServer = true; // neu la sv thi danh truoc
                pnlChessBoard.Enabled = true;
                socket.CreateServer();
            }
            else
            {
                socket.isServer = false;
                pnlChessBoard.Enabled = false;
                Listen();
            }
        }

        private void tmCountDown_Tick(object sender, EventArgs e)
        {
            // lam cho progressbar chay
            prgbCountDown.PerformStep();

            if (prgbCountDown.Value >= prgbCountDown.Maximum)
            {
                EndGame();
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point())); // gui lenh thong bao game bat dau 
            pnlChessBoard.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch { }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }


        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void txbIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(txbIP.Text))
            {
                txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }

        // tao ham lang nghe -> thuc hien rc
        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    SocketData data = (SocketData)socket.Receive();

                    ProcessData(data);
                }
                catch (Exception e)
                {
                }
            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        // xu li data nhan
        private void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY: // neu la NOTIFY thi show data message
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME: // neu la NEWGAME thi thuc hien..
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                        pnlChessBoard.Enabled = false;
                    }));
                    break;
                case (int)SocketCommand.SEND_POINT: // neu sv gui point thi thuc hien..
                    this.Invoke((MethodInvoker)(() => // chay k bi loi khi thay doi giao dien (tm)
                    {
                        prgbCountDown.Value = 0; // reset progress
                        pnlChessBoard.Enabled = true; // nhan point thi enable pnlChessBoard
                        tmCountDown.Start(); // tm start
                        ChessBoard.OtherPlayerMark(data.Point);
                        undoToolStripMenuItem.Enabled = true; // khi gui point se duoc undo
                    }));
                    break;
                case (int)SocketCommand.UNDO: 
                    Undo();
                    prgbCountDown.Value = 0;
                    break;
                case (int)SocketCommand.END_GAME:
                    MessageBox.Show("Đã đủ 5 quân cờ trên 1 hàng");
                    break;
                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("Hết giờ");
                    break;
                case (int)SocketCommand.QUIT:
                    tmCountDown.Stop();
                    MessageBox.Show("Người chơi đã thoát");
                    break;
                default:
                    break;
            }

            Listen(); // sv tiep tuc lang nghe khi cli truyen point
        }
        #endregion
    }
}
