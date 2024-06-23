using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    public class ChessBoardManager
    {
        #region Properties
        private Panel chessBoard;
        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        private List<Player> player;
        public List<Player> Player
        {
            get { return player; }
            set { player = value; }
        }

        // biến lưu lại người nào đang đánh
        private int currentPlayer;
        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        private TextBox playerName;
        public TextBox PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        private PictureBox playerMark;
        public PictureBox PlayerMark
        {
            get { return playerMark; }
            set { playerMark = value; }
        }
        #endregion

        // luu button vao trong mang -> tao List long trong List
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        // tao event nguoi choi da di nuoc co 
        private event EventHandler<ButtonClickEvent> playerMarked;
        public event EventHandler<ButtonClickEvent> PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }

        // tao event ket thuc game
        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }

        // tao stack luu toa do cua buoc di
        private Stack<PlayInfo> playTimeLine;

        public Stack<PlayInfo> PlayTimeLine
        {
            get { return playTimeLine; }
            set { playTimeLine = value; }
        }

        #region Initialize
        // hàm dựng
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox mark)
        {
            this.ChessBoard = chessBoard;
            this.PlayerName = playerName;
            this.PlayerMark = mark;
            //danh sách người chơi mặc định và ảnh con trỏ mỗi người
            this.Player = new List<Player>()
            {
                new Player("player1", Image.FromFile(Application.StartupPath + "\\Resources\\Shield.png")),
                new Player("player2", Image.FromFile(Application.StartupPath + "\\Resources\\Sword icon.png"))
            };


        }
        #endregion

        // block hàm
        #region Methods
        public void DrawChessBoard()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();

            PlayTimeLine = new Stack<PlayInfo>();

            CurrentPlayer = 0;
            // đổi người
            ChangePlayer();

            //khởi tạo biến Matrix, là một ma trận chứa các đối tượng Button.
            Matrix = new List<List<Button>>();

            // tạo 1 btn mẫu với vị trí 0 0
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            // vẽ chiều cao ( sau khi vẽ đủ chiều ngang sẽ xuống dòng vẽ chiều cao)
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                //tao mang moi luu lai cac btn
                Matrix.Add(new List<Button>());

                // vẽ chiều ngang
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        // mỗi ô sẽ có vị trí bằng vị trí ô trước cộng kích thước của ô đó
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString() //xac dinh 1 btn dang nam o hang i, cot j 
                    };

                    // sự kiện nhấn vào button
                    btn.Click += Btn_Click;

                    ChessBoard.Controls.Add(btn);

                    //hang thu i, add tat ca cac btn vo
                    Matrix[i].Add(btn);

                    oldButton = btn;
                }
                // sau khi đủ hàng, oldbutn có tọa độ X về 0 đầu dòng; Y cộng thêm chiều cao 1 bt
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                // kích thước oldbtn về 0 nếu k nó sẽ lùi cách 1 khoảng
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }

        // hàm sự kiện nhấn vào button
        // sender là th gửi event đi
        void Btn_Click(object? sender, EventArgs e)
        {
            //ép kiểu sender thành button gửi event đi
            Button btn = sender as Button;

            // đổi hình button
            if (btn.BackgroundImage != null)
                return;

            Mark(btn);

            // dua toa do buoc di vao stack
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer));

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;

            ChangePlayer();

            // ket thuc khong bi reset progressbar
            if (playerMarked != null)
                playerMarked(this, new ButtonClickEvent(GetChessPoint(btn)));

            //neu endgame chuyen den ham EndGame
            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        // truyen xuong point vi tri ng choi danh
        public void OtherPlayerMark(Point point)
        {
            Button btn = Matrix[point.Y][point.X];

            if (btn.BackgroundImage != null)
                return;

            Mark(btn);

            PlayTimeLine.Push(new PlayInfo(GetChessPoint(btn), CurrentPlayer));

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;

            ChangePlayer();

            if (isEndGame(btn))
            {
                EndGame();
            }
        }

        // ham EndGame
        public void EndGame()
        {
            if (endedGame != null)
                endedGame(this, new EventArgs());
        }

        // ham Undo
        public bool Undo()
        {
            if (PlayTimeLine.Count <= 0)
                return false;

            bool isUndo1 = UndoAStep();
            bool isUndo2 = UndoAStep();

            PlayInfo oldPoint = PlayTimeLine.Peek();
            CurrentPlayer = oldPoint.CurrentPlayer == 1 ? 0 : 1;

            return isUndo1 && isUndo2;
        }

        private bool UndoAStep()
        {
            if (PlayTimeLine.Count <= 0)
                return false;

            PlayInfo oldPoint = PlayTimeLine.Pop();
            Button btn = Matrix[oldPoint.Point.Y][oldPoint.Point.X];

            btn.BackgroundImage = null;

            if (PlayTimeLine.Count <= 0)
            {
                CurrentPlayer = 0;
            }
            else
            {
                oldPoint = PlayTimeLine.Peek();
            }

            ChangePlayer();

            return true;
        }

            // ham isEndGame kiem tra xem game ket thuc chua
            private bool isEndGame(Button btn)
        {
            // kiem tra ket thuc o hang ngang, hang doc, cheo chinh, cheo phu
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimary(btn) || isEndSub(btn);
        }

        // ham lay toa do cua 1 btn de kiem tra ket thuc
        private Point GetChessPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);

            Point point = new Point(horizontal, vertical);

            return point;
        }
        
        // xu li ket thuc o hang ngang
        private bool isEndHorizontal(Button btn)
        {
            Point point = GetChessPoint(btn); // lay toa do da xac dinh o ham tren ap dung vao

            // dem so btn giong nhau ben trai + phai = 5 thi ket thuc 
            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                    break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }

            return countLeft + countRight == 5;
        }

        // xu li ket thuc o hang doc 
        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn);

            // dem so btn giong nhau ben tren + duoi = 5 thi ket thuc 
            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }

        // xu li ket thuc o duong cheo chinh (toa do cung tang, cung giam)
        private bool isEndPrimary(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                // kiem tra xem co bi vuot khoi mang khong
                if (point.X - i < 0 || point.Y - i < 0)
                    break;

                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++) // o countTop tinh tu point.X roi nen o day la Cons. ... - point.X
            {
                // kiem tra xem co bi vuot khoi mang khong
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH)
                    break;

                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }

        // xu li ket thuc o duong cheo phu (neu di len be ngang tang, Y giam; neu di xuong thi nguoc lai)
        private bool isEndSub(Button btn)
        {
            Point point = GetChessPoint(btn);

            // TH di len
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > Cons.CHESS_BOARD_WIDTH || point.Y - i < 0)
                    break;

                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            // TH di xuong 
            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i < 0)
                    break;

                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom == 5;
        }

        // đổi hình button theo người chơi, sau đó ktra đổi ng chơi
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;  
        }

        // hiện tên người chơi và icon dấu của người đó lên tb và picturebox
        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;

            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }

        #endregion
    }

    // tao event luu toa do cua btn click
    public class ButtonClickEvent : EventArgs
    {
        private Point clickedPoint;

        public Point ClickedPoint
        {
            get { return clickedPoint; }
            set { clickedPoint = value; }
        }

        // ham dung
        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }

}
