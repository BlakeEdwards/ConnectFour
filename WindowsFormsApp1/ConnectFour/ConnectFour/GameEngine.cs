using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
        public class updateBoardArgs : EventArgs        // define my event args
        {
            public updateBoardArgs(Bitmap img) {this.img = img;}
            public Bitmap img{ get; set; }
        }
    class GameEngine
    {
        //public event EventHandler<MultipleOfFiveEventArgs> OnMultipleOfFiveReached;
        public event EventHandler<updateBoardArgs> onUpdate;        // define an event
        public int Height { private get; set; }
        public int Width { private get; set; }
        public int turn;
        public bool breaking;
        Board board;
        //int[,] boardState;

        public GameEngine(int hieght,int width)
        {
            turn = 1;
            Height = hieght;
            Width = width;
            board = new Board(Height, Width);
            breaking = false;
        }

        public Bitmap updateCanvas()
        {
            return board.DrawBoard();
            //onUpdate(this, new updateBoardArgs(board.DrawBoard()));
        }    
        //{ OnMultipleOfFiveReached(this, new MultipleOfFiveEventArgs(iSum));
        //Player Peice Hovering
        public Bitmap Hover(int playerId , int x, int y)
        {
            if (Properties.Settings.Default.userId == turn)
            {
                int col = board.getColumnNumber(x);
                //bound Col 0-6
                if (col > 6) col = 6;
                if (col < 0) col = 0;
                if (!move_Available(col)) return board.DrawBoard();
                int row = hover_Height(col);
                return board.playerHover(playerId, col, row);
            }
            else return board.DrawBoard();

        }
        private int hover_Height(int col)
        {
            for (int i = 0; i < 5; i++)
            {
                if (board.boardState[col, i+1] != 0)
                    return i+1;
            }
            return 6;
        }

        //Making a Move
        public bool Move(int playerId , int x, int y)
        {
            if (playerId != turn) return false ;     // if not players turn            
            int col = board.getColumnNumber(x);
            int row = board.getRowNumber(y);
            col = (col > 6) ? 6 : col;              // bound col so it can go over number of cols
            //check a place is available in Coloum
            if (!move_Available(col)) return false;
            else
            {
                row = addToCol(col,playerId);
                board.DrawBoard();
               turn *= -1;
            }
            return win(playerId,col, row);

        }          
        private bool move_Available(int col)
        {            
            for (int i = 0; i < 6; i++)
            {
                if (board.boardState[col, i] == 0) return true;
            }
            return false;
        }

        //add to coloum  at found row return the row height of placed peice
        private int addToCol(int col,int playerId)
        {
            for (int i = 0; i < 5; i++)
            {
                if (board.boardState[col,i+1] != 0)
                {
                    board.boardState[col, i] = playerId;                    
                    return i;
                }
            }
                board.boardState[col, 5] = playerId;
            return 5;
        }

        /*Check for Win 
         * 0 = no win
         * 1 = player 1
         * -1 = player 2
         */        
        public bool win(int playerId,int col,int row)
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

    public int Get_move()
    {
        Random rnd = new Random();
            int num;
            do
            {
                num = rnd.Next(7);
            }
            while (!move_Available(num));
        return num* (Width / 7);
    }
        public void reset(){board.boardState = new int[7, 6];}

    }

}
