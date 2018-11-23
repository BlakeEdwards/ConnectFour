using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class GameState
    {
        public int turn;
        public int[,] board;

        public GameState(int t, int[,] b)
        {
            this.board = new int[b.GetLength(0), b.GetLength(1)];
            copyArray(b, ref board);
                this.turn = t;
        }

        /// <summary>
        /// Given a Game file Will return a GameState Object
        /// </summary>
        /// <param name="file"></param>
        /// <returns>GameState</returns>
        public static GameState GetGameState(string file)
        {
            int t = int.Parse(file.Substring(0,file.IndexOf('/')));
            int[,] b = Get_IntArray(file.Substring(file.IndexOf('/')+1));
            GameState game = new GameState(t,b);
            return game;
        }
   
        /// <summary>
        /// Default game state with turn set 1 board size [7,6]
        /// </summary>
        /// <returns>GameState</returns>
        public static GameState GetGameState()
        {

            GameState game = new GameState(1, new int[7, 6]);
            return game;
        }

        private static void copyArray(int[,] b, ref int[,] board)
        {
            for (int dim1 = 0; dim1 < b.GetLength(0); dim1++)
            {
                for (int dim2 = 0; dim2 < b.GetLength(1);dim2++)
                {
                    board[dim1, dim2] = b[dim1, dim2];
                }
            }
        }

        private static int[,] Get_IntArray(string str)
        {
            int[,] array;
            string[] holder = str.Split('/');
            int col = int.Parse(holder[0]),
                row = int.Parse(holder[1]);
            array = new int[col, row];
            int cnt = 2;
            for (int i = 0; i < row; i++)
            {
                // Columns
                for (int j = 0; j < col; j++)
                {
                    array[j, i] = int.Parse(holder[cnt]);
                    cnt++;
                }
            }
            return array;
        }
        // create file string
        private static string Get_String(int[,] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.GetLength(1); i++)
            {
                // Columns
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (j != 0 || i != 0) sb.Append("/"); 
                    sb.Append(array[j, i]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Create a string value representing the GameState Object
        /// </summary>
        /// <returns>String</returns>
        public string Get_file()
        {
            string file = turn + "/" + board.GetLength(0) + "/" + board.GetLength(1) + "/";
            file += Get_String(board);
            return file;
        }


        // Over Rides
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;
            GameState p = obj as GameState;
            if ((System.Object)p == null)
                return false;

            return (turn == p.turn) && (board.Equals(p.board));
        }        
        public bool Equals(GameState p)
        {
            
            if ((object)p == null)
                return false;

            return (turn == p.turn) && (board.Equals(p.board));
        }
        public override int GetHashCode()
        {
            return turn.GetHashCode() ^ board.GetHashCode();
        }
        public static bool operator !=(GameState a, GameState b) => !(a == b);
        public static bool operator ==(GameState a, GameState b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            return (a.turn==b.turn && ArrayIsEqual(a.board,b.board))? true: false;
        }
        private static bool ArrayIsEqual(int[,] a,int[,] b)
        {
            if(a.GetLength(0) == b.GetLength(0) && a.GetLength(1) == b.GetLength(1))
            {
                for (int i = 0; i < a.GetLength(0) ; i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (a[i, j] != b[i, j]) return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
