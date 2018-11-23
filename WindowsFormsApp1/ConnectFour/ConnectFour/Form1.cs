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
        // create Event Form Will Publish
        private event EventHandler<CanvasClickedArgs> OnMoveMade;
        private event EventHandler<bool> OnAiChecked;
        public Form1()
        {      
            
            InitializeComponent();
            Player player = new Player("bob", Color.Blue);
            ChatScreen.Text = Player.Count.ToString();
            engine = new GameEngine(canvas.Size.Height, canvas.Size.Width);
            socket = new TcpConnect(4443);
            this.DoubleBuffered = true;
            //this.Paint += new PaintEventHandler(GameUpdate);  // subscribe to the form paint event and run our GameUpdate
            //canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_Mouse);
            //canvas.Enabled = false;
            // form Subsciptions
            canvas.MouseMove += new MouseEventHandler(Move_Mouse);
            canvas.MouseMove += new MouseEventHandler(engine.Hover);
            canvas.MouseLeave += new EventHandler(engine.clearHove);
            OnAiChecked += new EventHandler<bool>(engine.SetAi);
            OnMoveMade += new EventHandler<CanvasClickedArgs>(engine.gameBoard_Clicked);            

            // engine Subsciptions
            engine.OnWin += new EventHandler<string>(Winner);
            engine.OnUpdate += new EventHandler<Bitmap>(SetCanvas);
            engine.OnMoveMade += new EventHandler<MoveArgs>(socket.localMoveMade);            
            engine.reset();

            //netWork Subsciptions
            socket.OnError += new EventHandler<string>(ErrorBox);            
            socket.OnChatRecieved += new EventHandler<string>(chatRecieved);
            socket.OnMoveRecieved += new EventHandler<MoveArgs>(engine.Move);
        }

        

        private void SetCanvas(object sender, Bitmap img)
        {
            canvas.Image = img;
            this.Invalidate();
        }            
        private void canvas_Click(object sender, EventArgs e)
        {            
            OnMoveMade?.Invoke(this,new CanvasClickedArgs(x, y, Properties.Settings.Default.userId));
            //Properties.Settings.Default.userId *= -1;
        }
        private void Winner(object sender, string e)
        {
            System.Windows.Forms.MessageBox.Show("WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! WINNER!!! ");
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
        
        private void chatRecieved(object sender, string msg)
        {
            ChatScreen.Invoke((MethodInvoker)delegate
            {
                ChatScreen.Text += msg + "\n";
            });
        }
        
        // Menu Strip
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm options = new OptionsForm();
            options.Show();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame form2 = new NewGame();
            form2.Show();
        }

        // NetWorking Stuff
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
                    //socket = new TcpConnect(4443);
                    setupNetwork();
                }
                else
                {
                    MessageBox.Show("Please Enter IP Address");
                }
            }
        }
        private void clientCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (clientCheckBox.Checked) connectButton.Text = "Connect";
            else
            {
                connectButton.Text = "Host Game";
                try { socket.SetIp(""); } catch { }
            }
        }
        private void setupNetwork()
        {                        
            thread = new Thread(socket.Start);
            thread.Start();
        }
        private void disconectButton_Click(object sender, EventArgs e)
        {            
            socket.networkActive = false;
            if (thread.IsAlive)
            {
                socket.Dispose();
                Thread.Sleep(10);
                thread.Abort();
            }
        }


        // System handling
        private void ErrorBox(object sender, string error)
        {
            MessageBox.Show("Error: "+ error);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs evnt)
        {
            
            //Todo Save Board State if client is server 
            socket.OnError -= new EventHandler<string>(ErrorBox);            
            socket.OnChatRecieved -= new EventHandler<string>(chatRecieved);
            socket.OnMoveRecieved -= new EventHandler<MoveArgs>(engine.Move);
            try
            {
                socket.networkActive = false;                
            }
            catch (Exception e)
            { ErrorBox(this, e.ToString()); }
            try
            {
                if (thread.IsAlive)
                {
                    socket.Dispose();
                    Thread.Sleep(10);
                    thread.Abort();
                }
            }
            catch { }
        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            socket.Dispose();
            engine.Dispose();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void aiCheckBox_CheckedChanged(object sender, EventArgs e)
        {            
            OnAiChecked(this, aiCheckBox.Checked);
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.AddExtension = true;
            //saveFileDialog1.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int n = rnd.Next(0, 7);
            engine.Move(this, new MoveArgs(n, engine.turn));
        }

    }
}
