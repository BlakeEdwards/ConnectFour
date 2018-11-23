using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ConnectFour
{
    class Board
    {
        public int col { get; set; }
        public int[,] boardState { get; set; }
        public int MyProperty { get; set; }
        private static int columns = 7;
        private static int rows= 7;
        public static int nColumns { get { return columns; } }
        public static int nRows  { get { return rows; }}        
        private int bitMapHieght, bitMapWidth;
        private int radius;
        private int pad;
        // Display board
        public Board(int hieght, int width, int[,] board)
        {

            // create board
            boardState = new int[nColumns, nRows];
            copyArray(board, this.boardState);
            
            bitMapHieght = hieght;
            bitMapWidth = width;
            pad = 5;                // coishening for board spacing
            //create  Image
      

            radius = (bitMapHieght - (pad * 8)) / nColumns;   // although we only have 6 vertical slots the extra is for animation
            // the across the board we need to pad 8 gaps one in between each Column including the walls
        }
        private void copyArray(int[,] source, int[,] destination)
        {
            for (int i = 0; i < (nColumns-1); i++)
            {
                for (int j = 0; j < (nRows-1); j++)
                {
                    destination[i, j] = source[i, j];
                }
            }
        }
        // get the middle column x cord of current mouse x column
        private int getColumnCord(int col)
        {
            // devide the canvase width by number of columns
            // we add the pad to push off from the right padding
            return col * (bitMapWidth / nColumns) + pad;
        }
        public int getColumnNumber(int x)
        {
            return (x / (bitMapWidth / nColumns));
        }
        public int getRowNumber(int y)
        {
            //return (y / (bitMapHieght / 7))<7? (y / (bitMapHieght / 7)) > 1 ? y/(bitMapHieght / 7):1: 6;
            return y / (bitMapHieght / (nRows+1));
        }
        private int getRowCord(int row)
        {
            // devide the canvase width by number of columns
            // we add the pad to push off from the right padding
            return row * (bitMapHieght / nRows);
        }
        public void setHieght(int hieght)
        {
            bitMapHieght = hieght;
        }
        public int getHieght() => bitMapHieght;
        public int getWidth() => bitMapWidth;
    }
}
