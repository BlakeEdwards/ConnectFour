using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Form1 : Form
    {            
        //Board gameBoard;
        GameEngine engine;
        TcpConnect socket;
        Thread thread;
        private int x, y ;
        private event EventHandler<CanvasClickedArgs> OnMoveMade;
        public Form1()
        {            
            InitializeComponent();

            
            engine = new GameEngine(canvas.Size.Height, canvas.Size.Width);
            socket = new TcpConnect(4443);
            this.DoubleBuffered = true;
            //this.Paint += new PaintEventHandler(GameUpdate);  // subscribe to the form paint event and run our GameUpdate
            canvas.MouseMove += new MouseEventHandler(Move_Mouse);      // update mouse x/y.
            canvas.MouseMove += new MouseEventHandler(PlayerHover);
            canvas.MouseLeave += new EventHandler(engine.clearHove);
            canvas.Click += new EventHandler(this.canvas_Click);

            this.OnMoveMade += new EventHandler<CanvasClickedArgs>(engine.Move);
            engine.OnWin += new EventHandler<string>(Winner);
            engine.OnUpdate += new EventHandler<Bitmap>(SetCanvas);
            engine.reset();

            socket.OnError += new EventHandler<string>(ErrorBox);
            socket.OnSetTurn += new EventHandler<int>(engine.setTurn);
        }


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

        private void PlayerHover(object sender, EventArgs e)
        {            
            engine.Hover(Properties.Settings.Default.userId, x, y);
        }        

        private void canvas_Click(object sender, EventArgs e)
        {            
            OnMoveMade(this,new CanvasClickedArgs(x, y, Properties.Settings.Default.userId));            
        }

        private void Winner(object sender, string e)
        {
            System.Windows.Forms.MessageBox.Show("WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! ");
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (!socket.networkActive)
            {
                if (clientCheckBox.Checked && ipInput.Text != "")
                {
                    // set socket ip and make it a client
                    socket.SetIp(ipInput.Text);
                    setupNetwork();
                }
                else if (!clientCheckBox.Checked)
                {
                    socket = new TcpConnect(4443);
                    setupNetwork();
                }
                else
                {
                    MessageBox.Show("Please Enter IP Address");
                }
            }
        }

        private void setupNetwork()
        {                        
            thread = new Thread(socket.Start);
            thread.Start();
        }

        // Todo
        private void chatRecieved(object sender, string msg)
        {
            ChatScreen.Invoke((MethodInvoker)delegate
            {
                ChatScreen.Text += msg + "\nupdated from network thread";
            });
        }

        private void ResetBoard(object sender, EventArgs e)
        {
            engine.reset();
        }

        private void Move_Mouse(object sender, MouseEventArgs e)
        {
            
            x = e.X;
            y = e.Y;            
            //Invalidate();
        }



        private void ErrorBox(object sender, string error)
        {
            MessageBox.Show("Error: "+ error);
        }
    }
}
