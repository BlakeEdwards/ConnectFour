using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Form1 : Form
    {            
        //Board gameBoard;
        GameEngine engine;
        private int x, y, playerId;
        private event EventHandler<CanvasClickedArgs> OnMoveMade;
        public Form1()
        {
            
            InitializeComponent();

            playerId = Properties.Settings.Default.userId;
            engine = new GameEngine(canvas.Size.Height, canvas.Size.Width);
            this.DoubleBuffered = true;
            //this.Paint += new PaintEventHandler(GameUpdate);  // subscribe to the form paint event and run our GameUpdate
            canvas.MouseMove += new MouseEventHandler(Move_Mouse);      // update mouse x/y.
            canvas.MouseMove += new MouseEventHandler(PlayerHover);
            canvas.MouseLeave += new EventHandler(ClearHover);

            this.OnMoveMade += new EventHandler<CanvasClickedArgs>(engine.Move);
            engine.OnWin += new EventHandler<string>(Winner);
            engine.OnUpdate += new EventHandler<Bitmap>(SetCanvas);
            canvas.Image = engine.UpdateCanvas();
        }

        //private void GameUpdate(object sender, PaintEventArgs e)
        //{            
        // //   canvas.Image = gameBoard.DrawBoard();
        //}
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame form2 = new NewGame();
            form2.Show();
        }

        private void SetCanvas(object sender, Bitmap img)
        {
            canvas.Image = img;
            this.Invalidate();
        }

        private void ClearHover(object sender, EventArgs e)
        {
               canvas.Image = engine.UpdateCanvas();
        }
        private void PlayerHover(object sender, EventArgs e)
        {
            // calculate col and row
            int col = x / (canvas.Width / 7), row = (y / (canvas.Height / 7)) - 1;
            col = (col > 6) ? col = 6 : col;
                        //display Info on chat window....
            ChatScreen.Text = "mouse x = " + x + " Mouse y = " + y+ "\nColumn number: " + col + "\tRow number: " + row+"\n "
                +"Player turn = "+engine.turn;
            //Call for player Peice to hover at mouse            
            //canvas.Image.Dispose();
            canvas.Image = engine.Hover(Properties.Settings.Default.userId, x, y);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                { Properties.Settings.Default.userId = 1;
                lblTurn.Text = "Player 1";
            }
            else
                { Properties.Settings.Default.userId = -1;
                lblTurn.Text = "Player 2";
            }
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            playerId = Properties.Settings.Default.userId;
            OnMoveMade(this,new CanvasClickedArgs(x, y, playerId));
            Properties.Settings.Default.userId *= -1;
        }

        private void Winner(object sender, string e)
        {
            System.Windows.Forms.MessageBox.Show("WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! ");
        }
        private void resetBoard(object sender, EventArgs e)
        {
            engine.reset();
            canvas.Image = engine.UpdateCanvas();
        }

        private void Move_Mouse(object sender, MouseEventArgs e)
        {
            
            x = e.X;
            y = e.Y;            
            //Invalidate();
        }

    }
}
