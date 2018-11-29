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
       // public event EventHandler<Bitmap> OnUpdate;        // define an event
        public event EventHandler<Board> OnUpdate;        // define an event
        public event EventHandler<MoveArgs> OnMoveMade;        
        public event EventHandler<string> OnWin;        
        private int Width;        
        public bool gameActive { get; private set; }
        public bool Ai { get; set; }     // attach to player object
        public int Turn { get; set; }        
        private Board board;

        /// <summary>
        /// /// Initiate with a random player to Start
        /// </summary>
        /// <param name="hieght"></param>
        /// <param name="width"></param>
        public GameEngine(int hieght,int width)
        {                           
            setUpEngine(hieght, width, 0,new int[Board.nColumns, Board.nRows]);
        }        
        /// <summary>
        /// Initiate with a set player to Start
        /// </summary>
        /// <param name="hieght"></param>
        /// <param name="width"></param>
        /// <param name="turn"></param>
        public GameEngine(int hieght, int width,int turn)
        {
            setUpEngine(hieght, width, turn, new int[Board.nColumns, Board.nRows]);
        }

        /// <summary>
        /// Start Game With a loaded Board
        /// </summary>
        /// <param name="hieght"></param>
        /// <param name="width"></param>
        /// <param name="turn"></param>
        /// <param name="boardState"></param>
        public GameEngine(int hieght, int width, int turn, int[,] boardState)
        {
            setUpEngine(hieght, width, turn, boardState);
        }

        public void StartGame(GameState gameState)
        {
            gameActive = true;
            board.boardState = gameState.board;
            Turn = gameState.turn;
            updateEvent();
        }

        public void setUpEngine(int hieght, int width, int turn, int[,] boardState)
        {            
            Ai = false;
            this.Turn = turn;
            Width = width;
            // Todo use this to test if game is active and enable /diable engine.Onupdate.
            
            board = new Board(hieght, Width,boardState);
            OnUpdate?.Invoke(this, board);
        }
        
        public void Hover(object obj, MouseEventArgs e)
        {
            if (Properties.Settings.Default.userId == Turn)
            {
                // get col number
                int col = board.getColumnNumber(e.X);
                //bound Col 0-6
                if (col > Board.nColumns-1) col = Board.nColumns-1;
                if (col < 0) col = 0;
                int row = Hover_Height(col);
                lock (board)        // LockBoard while in use
                {
                    //OnUpdate?.Invoke(this, board.playerHover(Properties.Settings.Default.userId, col, row));
                    board.boardState[col, 0] = Turn;
                    updateEvent();
                    board.boardState[col, 0] = 0;
                }
                return;
            }
            else return; ;
        }

        internal void setTurn(object sender, int e)
        {
            Turn = e;

        }

        //public void clearHove(object sender, EventArgs e) => OnUpdate?.Invoke(this, board.getBoardImg());
        public void clearHove(object sender, EventArgs e) => updateEvent();
        //Making a Move
        public void gameBoard_Clicked(object sender, CanvasClickedArgs e)
        {                        
            if (e.playerId != Turn) return;     // not players turn Return
            int col = board.getColumnNumber(e.x);
            int row = board.getRowNumber(e.y);
            col = (col > (Board.nColumns-1)) ? (Board.nColumns - 1) : col;              // bound col so it can go over number of cols
            this.Move(this, new MoveArgs(col, e.playerId));
        }
        public void Move(object sender, MoveArgs e)
        {
            if (e.playerId != Turn) return;     // not players turn Return
            int col = e.col;
            int playerId = e.playerId;
            int row;
            //check a place is available in Coloum
            if (!Move_Available(col)) return; // Move Not Avaliable Return 
            
            //annoyouns Move to network if it was made by local player
            if (e.playerId == Properties.Settings.Default.userId) OnMoveMade?.Invoke(this, new MoveArgs(col, Properties.Settings.Default.userId));
            row = GetRow(col,playerId);
            lock (board)        // Lock Board while in use
            {
                board.boardState[col, row] = e.playerId;      //update Boardstate With move
                OnUpdate(this, board);
            }
            Turn *= -1;   // use
                          // use  reference types that point to player / players peices ------------------------------------------------------
                          // dont repeat your selfs

            // Ai Takes over moves
            
            if (win(e.playerId, col, row))
            {
                gameActive = false;
                OnWin?.Invoke(this, e.playerId.ToString()); // Publish Win Event
            }
            if (Ai && gameActive && Turn == Properties.Settings.Default.userId)
            {
                AiPlayer();
            }
        }

        /// <summary>
        /// Returns Current Board Array
        /// </summary>
        /// <returns>int32[,]</returns>
        public int[,] GetBoard() => board.boardState;

        public void reset()
        {
            int h = board.getHieght();
            int w = board.getWidth();            
            board = new Board(h, w,new int[Board.nColumns, Board.nColumns]);
            updateEvent();
        }
        
        // Helper Classes
        private int Hover_Height(int col)
        {
            for (int i = 0; i < (Board.nRows-1); i++)
            {
                if (board.boardState[col, i+1] != 0)
                    return i+1;
            }
            return 6;
        }

        private bool Move_Available(int col) => board.boardState[col, 1] == 0 ?  true: false;
        
        private int GetRow(int col,int playerId)
        {
            for (int i = 1; i < (Board.nRows-1); i++)
            {
                if (board.boardState[col,i+1] != 0)
                {
                    return i;
                }
            }
            return Board.nRows-1;
        }

        /*Check for Win 
         * 0 = no win
         * 1 = player 1
         * -1 = player 2
         */
        internal GameState GetGameState() => new GameState(Turn, board.boardState);
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
            //Todo Make row and column  into variable constants
            int ctn = 0;            
            for (int i = 1; i < 7; i++)// row
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
            //changed y here to zero for the hover Bounds
            while (x > -1 && y>-0 && board.boardState[x, y]== playerId)
            {
                ctn++;
                x--;y--;
            }
            x = col+1; y = row+1;
            while (x < 7 && y < 7 && board.boardState[x, y] == playerId)
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
            while (x > -1 && y < 7 && board.boardState[x, y] == playerId)
            {
                ctn++;
                x--; y++;
            }
            x = col + 1; y = row-1;
            while (x < 7 && y > -0 && board.boardState[x, y] == playerId)
            {
                ctn++;
                x++; y--;
            }
            if (ctn >= 4) return true;
            return false;            
        }

        // Getting Ai Move
        private int Get_move()
        {
            Random rnd = new Random();
            int num;
            do
            {
                num = rnd.Next(7);
            }
            while (!Move_Available(num));
            return num;
        }
        private void AiPlayer()
        {                       
            Move(this, new MoveArgs(Get_move(), Properties.Settings.Default.userId));                
        }
        public void SetAi(object obj,bool value)
        {
            Ai = value;
            if(Ai && gameActive && Turn == Properties.Settings.Default.userId) { AiPlayer(); }
        }

        private void updateEvent()
        {
            OnUpdate?.Invoke(this, board);
        }
    }

}
