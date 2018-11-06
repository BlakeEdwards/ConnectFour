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
        private Color player1Color = Color.Red;
        private Color player2Color = Color.Yellow;
        private Color backColor = SystemColors.ActiveBorder;
        private Color boardColor = Color.Blue;
        private int bitMapHieght, bitMapWidth;
        private int radius;
        private Bitmap DrawArea;
        private int pad;
        // Display board
        public Board(int hieght, int width)
        {
            boardState = new int[7, 6];     // board is 7 across and 6 high
            bitMapHieght = hieght;
            bitMapWidth = width;
            pad = 5;                // coishening for board spacing
            DrawArea = new Bitmap(bitMapWidth, bitMapHieght);            

            radius = (bitMapHieght - (pad * 8)) / 7;   // although we only have 6 vertical slots the extra is for animation
            // the across the board we need to pad 8 gaps one in between each Column including the walls
            LoadBoardImg(boardState);
        }

        //public Bitmap DrawBoard(int[][] board)
        private Bitmap LoadBoardImg(int[,] board)
        {
            // each square size
            //Bitmap DrawArea = new Bitmap(bitMapWidth, bitMapHieght);

            Graphics g = Graphics.FromImage(DrawArea);
            // Background 
            g.FillRectangle(new SolidBrush(backColor), 0, 0, bitMapHieght, bitMapWidth);
            // Board
            g.FillRectangle(new SolidBrush(boardColor), 0, radius + (pad / 2), bitMapWidth, radius * 7);

            Brush brush = new SolidBrush(backColor);
            // create holes to look like a board
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    AddPeice(x, y);
                }
            }
            g.Dispose();

            return DrawArea;
        }

        public void AddPeice(int col, int row)
        {
            Graphics g = Graphics.FromImage(DrawArea);
            Brush brush;
            switch (boardState[col, row])
            {
                case 1:
                    brush = new SolidBrush(player1Color);
                    break;
                case -1:
                    brush = new SolidBrush(player2Color);
                    break;
                default:
                    brush = new SolidBrush(backColor);
                    break;
            }
            g.FillEllipse(brush,
                col * radius + (pad * col) + pad,
                row * radius + (pad * (row + 1)) + radius,
                radius, radius
                );
        }

        public Bitmap playerHover(int player, int col, int row)
        {
            Bitmap DrawHover = (Bitmap)DrawArea.Clone();
            //DrawArea.MakeTransparent(Color.Aqua);
            Graphics g = Graphics.FromImage(DrawHover);
            Color color = player == 1 ? player1Color : player2Color;
            // --- 3 Different ways to draw a player Hovering
            //g.FillEllipse(new SolidBrush(color), col - (radius/2), 0, radius, radius);  // draw hover at x
            g.FillEllipse(new SolidBrush(color), getColumnCord(col), 0, radius, radius); // Draw hover at Column                
                                                                                         //g.FillEllipse(new SolidBrush(color),getColumnCord(col), getRowCord(row), radius, radius);// Draw at col and row

            g.Dispose();
            return DrawHover;
        }

        public Bitmap getBoardImg() => DrawArea;
        // get the middle column x cord of current mouse x column
        private int getColumnCord(int col)
        {
            // devide the canvase width by number of columns
            // we add the pad to push off from the right padding
            return col * (bitMapWidth / 7) + pad;
        }
        public int getColumnNumber(int x)
        {
            return (x / (bitMapWidth / 7));
        }

        public int getRowNumber(int y)
        {
            //return (y / (bitMapHieght / 7))<7? (y / (bitMapHieght / 7)) > 1 ? y/(bitMapHieght / 7):1: 6;
            return y / (bitMapHieght / 7);
        }
        private int getRowCord(int row)
        {
            // devide the canvase width by number of columns
            // we add the pad to push off from the right padding
            return row * (bitMapHieght / 7);
        }
        public void setHieght(int hieght)
        {
            bitMapHieght = hieght;
        }
        public int getHieght() => bitMapHieght;
        public int getWidth() => bitMapWidth;

        public Bitmap GetImg() => DrawArea;

    }
}
