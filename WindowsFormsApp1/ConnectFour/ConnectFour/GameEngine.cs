using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    class GameEngine
    {
        //public event EventHandler<MultipleOfFiveEventArgs> OnMultipleOfFiveReached;
        public event EventHandler<Bitmap> OnUpdate;        // define an event
        public event EventHandler<MoveArgs> OnMoveMade;
        
        public event EventHandler<string> OnWin;
        private int Width;
        // Todo make turn private and clean up
        public bool gameActive { get; private set; }
        public bool Ai { get; set; }
        public int turn;
        private Board board;

        public GameEngine(int hieght,int width)
        {
            setUpEngine(hieght, width, 1,new int[7, 6]);            
        }
        public GameEngine(int hieght, int width,int turn)
        {
            setUpEngine(hieght, width, turn, new int[7, 6]);
        }
        public GameEngine(int hieght, int width, int turn, int[,] boardState)
        {
            setUpEngine(hieght, width, turn, boardState);
        }
        public void setUpEngine(int hieght, int width, int turn, int[,] boardState)
        {
            Ai = false;
            this.turn = turn;
            Width = width;
            // Todo use this to test if game is active and enable /diable engine.Onupdate.
            gameActive = true;
            board = new Board(hieght, Width,boardState);
            OnUpdate?.Invoke(this, board.getBoardImg());
        }
        //Player Peice Hovering
        /// <summary>
        /// Will update a board img with player peice hoving at mouse (x,y)
        /// subcribe to OnUpdate to recieve img
        /// </summary>
        /// <param name="playerId">player Id</param>
        /// <param name="x">Mouse x on Board</param>
        /// <param name="y">Mouse x on Board</param>
        public void Hover(object obj, MouseEventArgs e)
        {
            if (Properties.Settings.Default.userId == turn)
            {
                // get col number
                int col = board.getColumnNumber(e.X);
                //bound Col 0-6
                if (col > 6) col = 6;
                if (col < 0) col = 0;
                if (!Move_Available(col)) return;

                int row = Hover_Height(col);
                lock (board)        // LockBoard while in use
                {
                    
                    OnUpdate?.Invoke(this, board.playerHover(Properties.Settings.Default.userId, col, row));
                }
                return;
            }
            else return; ;
        }

        internal void setTurn(object sender, int e)
        {
            turn = e;

        }

        public void clearHove(object sender, EventArgs e) => OnUpdate?.Invoke(this, board.getBoardImg());
        //Making a Move
        public void gameBoard_Clicked(object sender, CanvasClickedArgs e)
        {                        
            if (e.playerId != turn) return;     // not players turn Return
            int col = board.getColumnNumber(e.x);
            int row = board.getRowNumber(e.y);
            col = (col > 6) ? 6 : col;              // bound col so it can go over number of cols
            this.Move(this, new MoveArgs(col, e.playerId));
        }
        public void Move(object sender, MoveArgs e)
        {
            if (e.playerId != turn) return;     // not players turn Return
            int col = e.col;
            int playerId = e.playerId;
            int row;
            //check a place is available in Coloum
            if (!Move_Available(col)) return; // Move Not Avaliable Return 
            else
            {
                //annoyouns Move to network if it was made by local player
                if (e.playerId == Properties.Settings.Default.userId) OnMoveMade?.Invoke(this, new MoveArgs(col, Properties.Settings.Default.userId));
                row = GetRow(col,playerId);
                lock (board)        // Lock Board while in use
                {
                    board.boardState[col, row] = e.playerId;      //update Boardstate With move
                    board.AddPeice(col, row);
                    //OnUpdate(this, board.getBoardImg());
                    OnUpdate?.Invoke(this, board.getBoardImg());
                }
                turn *= -1;
                // Ai Takes over moves
                if (Ai && turn == Properties.Settings.Default.userId)
                {
                    AiPlayer();
                }
            }
            if (win(e.playerId, col, row))
            {
                gameActive = false;
                OnWin?.Invoke(this, e.playerId.ToString()); // Publish Win Event
            }

        }

        public void reset()
        {
            int h = board.getHieght();
            int w = board.getWidth();            
            board = new Board(h, w,new int[Board.nColumns, Board.nColumns]);
            OnUpdate?.Invoke(this, board.getBoardImg());
        }
        
        // Helper Classes
        private int Hover_Height(int col)
        {
            for (int i = 0; i < 5; i++)
            {
                if (board.boardState[col, i+1] != 0)
                    return i+1;
            }
            return 6;
        }
        private bool Move_Available(int col)
        {            
            for (int i = 0; i < 6; i++)
            {
                if (board.boardState[col, i] == 0) return true;
            }
            return false;
        }
        private int GetRow(int col,int playerId)
        {
            for (int i = 0; i < 5; i++)
            {
                if (board.boardState[col,i+1] != 0)
                {
                    return i;
                }
            }
            return 5;
        }

        /*Check for Win 
         * 0 = no win
         * 1 = player 1
         * -1 = player 2
         */        

        private bool win(int playerId,int col,int row)
        {            
            if (vert_win(playerId,col)) return true;
            if (horz_win(playerId, col, row)) return true;
            if (tlbr_win(playerId, col, row)) return true;
            if (bltr_win(playerId, col, row)) return true;            
            return false;
        }
        // Vertical Win
        private bool vert_win(int playerId,int col)
        {
            int ctn = 0;            
            for (int i = 0; i < 6; i++)
            {
                if (board.boardState[col, i] == playerId * -1) break;
                else if (board.boardState[col, i] == playerId) ctn ++; 
            }
            if (ctn >= 4) return true;
            return false;
        }
        // Horizontal Win
        private bool horz_win(int playerId, int col, int row)
        {
            int ctn = 0, i= col-1;
            // Left scan
            while (i > -1 && board.boardState[i,row]==playerId )
            {
                ctn++;
                i--;
            }

            i = col + 1;    //reset i to point just after placed ppeice
            // Right scan
            while (i < 7 && board.boardState[i, row] == playerId)
            {
                ctn++;
                i++;
            }
            if (ctn >= 3) return true;
            return false;
        }        
        //Top Left to Bottom Right Win
        private bool tlbr_win(int playerId, int col, int row)
        {
            
            int x = col, y = row,ctn = 0;
            while(x > -1 && y>-1 && board.boardState[x, y]== playerId)
            {
                ctn++;
                x--;y--;
            }
            x = col+1; y = row+1;
            while (x < 7 && y < 6 && board.boardState[x, y] == playerId)
            {
                ctn++;
                x++; y++;
            }
            if (ctn >= 4) return true;
            return false;
        }
        //Bottom left to Top Right Win
        private bool bltr_win(int playerId, int col, int row)
        {
            int x = col, y = row, ctn = 0;            
            while (x > -1 && y < 6 && board.boardState[x, y] == playerId)
            {
                ctn++;
                x--; y++;
            }
            x = col + 1; y = row-1;
            while (x < 7 && y > -1 && board.boardState[x, y] == playerId)
            {
                ctn++;
                x++; y--;
            }
            if (ctn >= 4) return true;
            return false;            
        }

        private int Get_move()
        {
            Random rnd = new Random();
            int num;
            do
            {
                num = rnd.Next(7);
            }
            while (!Move_Available(num));
            return num * (Width / 7);
        }
        private void AiPlayer()
        {
            // Todo impliment Ai logic    
        }
        public void SetAi(object obj,bool value)
        {
            Ai = value;
        }

    }

}
