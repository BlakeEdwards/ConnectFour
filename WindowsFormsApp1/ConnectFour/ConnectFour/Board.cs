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
        private Color player1Color = Color.Blue;
        private Color player2Color = Color.Yellow;
        private int bitMapHieght, bitMapWidth;
        private int radius;
        private int pad;        
        // Display board
        public Board(int hieght,int width)
        {
            boardState = new int[7,6];     // board is 7 across and 6 high
            bitMapHieght = hieght;
            bitMapWidth = width;
            pad = 5;                // coishening for board spacing

            radius = (bitMapHieght - (pad*8)) / 7;   // although we only have 6 vertical slots the extra is for animation
            // the across the board we need to pad 8 gaps one in between each Column including the walls
        }

        // ToDo
        private int getColumHieght(int col)
        {
            return 0;
        }
        //public Bitmap DrawBoard(int[][] board)

        public Bitmap DrawBoard()
        {
            
            // each square size
            //Bitmap DrawArea = new Bitmap(bitMapWidth, bitMapHieght);
            Bitmap DrawArea = new Bitmap(bitMapWidth, bitMapHieght);            
            Graphics g = Graphics.FromImage(DrawArea);
            g.FillRectangle(new SolidBrush(Color.DarkGreen), 0, radius+(pad/2), bitMapWidth, radius * 7);
            Brush brush;

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x ++)
                {
                    switch (boardState[x,y])
                    {                        
                        case 1:
                            brush = new SolidBrush(player1Color);
                            break;
                        case -1:
                            brush = new SolidBrush(player2Color);
                            break;
                        default:
                            brush = new SolidBrush(Color.Aqua);
                            break;
                    }                    
                    g.FillEllipse(brush, 
                        x * radius + (pad * x)+pad, 
                        y * radius + (pad * (y + 1)) + radius,
                        radius, radius
                        );                                        
                }
            }
            g.Dispose();
            
            return DrawArea;
        }

        public Bitmap playerHover(int player,int col , int row)
        {
            Bitmap DrawArea = DrawBoard(); 
            //DrawArea.MakeTransparent(Color.Aqua);
                Graphics g = Graphics.FromImage(DrawArea);
            // hove will only display if it is currently that players turn
            // UserId is set based on host vs client : if solo-> random            
            {
                Color color = player == 1 ? player1Color : player2Color;                       
                // 3 Different ways to draw a player Hovering
                //g.FillEllipse(new SolidBrush(color), col - (radius/2), 0, radius, radius);  // draw hover at x
                g.FillEllipse(new SolidBrush(color), getColumnCord(col), 0, radius, radius); // Draw hover at Column                
                //g.FillEllipse(new SolidBrush(color),getColumnCord(col), getRowCord(row), radius, radius);// Draw at col and row
            }
            g.Dispose();
            return DrawArea;
        }
     
        // get the middle column x cord of current mouse x column
        private int getColumnCord(int col)
        {     
            // devide the canvase width by number of columns
            // we add the pad to push off from the right padding
            return col * (bitMapWidth / 7)+pad;
        }        
        // get Column number of where mouse is on our canvas
        // numbers ranging from 0 to 6
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

    }
}
