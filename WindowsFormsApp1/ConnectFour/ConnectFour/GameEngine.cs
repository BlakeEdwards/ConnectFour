using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class GameEngine
    {
        //public event EventHandler<MultipleOfFiveEventArgs> OnMultipleOfFiveReached;
        public event EventHandler<Bitmap> OnUpdate;        // define an event
        public event EventHandler<MovedMadeArgs> OnMoveMade;
        public event EventHandler<string> OnWin;
        private int Width;
        // Todo make turn private and clean up
        public int turn;
        public bool gameActive { get; private set; }
        private Board board;

        public GameEngine(int hieght,int width)
        {
            turn = 1;
            Width = width;
            gameActive = true;
            board = new Board(hieght, Width);
            OnUpdate?.Invoke(this, board.getBoardImg());
        }

        //Player Peice Hovering
        public void Hover(int playerId , int x, int y)
        {
            if (Properties.Settings.Default.userId == turn)
            {
                // get col number
                int col = board.getColumnNumber(x);
                //bound Col 0-6
                if (col > 6) col = 6;
                if (col < 0) col = 0;
                if (!Move_Available(col)) return;

                int row = Hover_Height(col);
                lock (board)        // LockBoard while in use
                {
                    
                    OnUpdate?.Invoke(this, board.playerHover(playerId, col, row));
                }
                return;
            }
            else return; ;
        }
        public void clearHove(object sender, EventArgs e) => OnUpdate?.Invoke(this, board.getBoardImg());
        //Making a Move
        public void Move(object sender, CanvasClickedArgs e)
        {
            
            if (e.playerId != turn) return ;     // not players turn Return
            int col = board.getColumnNumber(e.x);
            int row = board.getRowNumber(e.y);
            col = (col > 6) ? 6 : col;              // bound col so it can go over number of cols
            //check a place is available in Coloum
            if (!Move_Available(col)) return; // Move Not Avaliable Return 
            else
            {
                row = GetRow(col,e.playerId);
                lock (board)        // Lock Board while in use
                {
                    board.boardState[col, row] = e.playerId;      //update Boardstate With move
                    board.AddPeice(col, row);
                    //OnUpdate(this, board.getBoardImg());
                    OnUpdate?.Invoke(this, board.getBoardImg());
                }
                turn *= -1;
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
            board = new Board(h, w);
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
        return num* (Width / 7);
    }

    }

}
