using BinaryFileHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Form1 : Form 
    {            
        //Board gameBoard;
        GameEngine engine;
        Image boardImage;
        Image coin1;
        Image coin2;
        Image coinDefault;
        private const char paramDelimiter = '|';
        private const int PAD = 5;
        private const int COLUMNS = 7;
        private const int ROWS = 6;
        private Regex ipPattern = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        private int radius;
        
        private TcpConnect socket;

        Thread thread;
        private int x, y ;
        // create Event Form Will Publish
        private event EventHandler<CanvasClickedArgs> OnMoveMade;
        private event EventHandler<bool> OnAiChecked;
        public Form1()
        {      
            
            InitializeComponent();      
            radius = (canvas.Height - (PAD * 8)) / COLUMNS;
            //Processor processor = new Processor();

            socket = new TcpConnect(4443);                       
            engine = new GameEngine(canvas.Size.Height, canvas.Size.Width);
            initializeImages();


            this.DoubleBuffered = true;
            // form Subsciptions
            // Input Keybinding enter key to Buttons
            messageInput.GotFocus += delegate (object sender, EventArgs e) { this.AcceptButton = SendMsgButton; };
            ipInput.GotFocus += delegate (object sender, EventArgs e) { this.AcceptButton = connectButton; };            
            // Todo Redefine these to call  Processor methods
            canvas.MouseMove += new MouseEventHandler(Move_Mouse);
            canvas.MouseMove += new MouseEventHandler(engine.Hover);
            canvas.MouseLeave += new EventHandler(engine.clearHove);
            OnAiChecked += new EventHandler<bool>(engine.SetAi);
            OnMoveMade += new EventHandler<CanvasClickedArgs>(engine.gameBoard_Clicked);

            // Netwok Subscrritions
            socket.OnError += new EventHandler<string>(ErrorBox);
            socket.OnChatRecieved += new EventHandler<string>(chatRecieved);
            socket.OnMoveRecieved += new EventHandler<MoveArgs>(engine.Move);
            socket.OnNewGame += new EventHandler<GameState>(startGame);
            socket.OnDisconnect += new EventHandler(handDisiconnection);
            socket.OnConnected += new EventHandler(handConnected);

            // ToDo  Plugin Engine Events That the processor will be listening to do something
            // engine Subsciptions

            engine.OnWin += new EventHandler<string>(Winner);
            engine.OnUpdate += new EventHandler<Board>(SetCanvas);
            engine.OnMoveMade += new EventHandler<MoveArgs>(socket.localMoveMade);
            engine.reset();
    
        }

        private void handConnected(object sender, EventArgs e)
        {
            clientCheckBox.Invoke((MethodInvoker)delegate
            {
                clientCheckBox.Enabled = false;
            });
        }

        private void handDisiconnection(object sender, EventArgs e)
        {
            clientCheckBox.Invoke((MethodInvoker)delegate
            {
                clientCheckBox.Enabled = true;
            });
        }

        public void initializeImages()
        {
            if(coin1 != null)
            {
                coin1.Dispose();
                coin2.Dispose();
                coinDefault.Dispose();
                coin1 = null;
                coin2 = null;
                coinDefault = null;
            }
            coin1 = drawCoin(Properties.Settings.Default.play1Col);
            coin2 = drawCoin(Properties.Settings.Default.play2Col);
            coinDefault = drawCoin(Properties.Settings.Default.backGroundColor);
            if (boardImage == null)
                { boardImage = new Bitmap(canvas.Width, canvas.Height); }
            else
            {
                boardImage.Dispose();
                boardImage = new Bitmap(canvas.Width, canvas.Height);
            }

            Graphics g = Graphics.FromImage(boardImage);
            // Background 
            g.FillRectangle(new SolidBrush(Properties.Settings.Default.backGroundColor), 0, 0, canvas.Height, canvas.Width);
            // Board
            g.FillRectangle(new SolidBrush(Properties.Settings.Default.boardColor), 0, radius + (PAD / 2), canvas.Width, radius * COLUMNS);
            g.Dispose();
            LoadBoardImg(engine.GetBoard());
            canvas.Image = boardImage;
        }
        private Image drawCoin(Color color)
        {
            Bitmap coin = new Bitmap(radius, radius);
            Graphics g = Graphics.FromImage(coin);
            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, new Rectangle(0, 0, radius, radius));            
            return coin;
        }
        private void LoadBoardImg(int[,] board)
        {                        
            for (int y = 0; y < ROWS+1; y++)
            {
                for (int x = 0; x < COLUMNS; x++)
                {
                    AddPeice(board ,x, y);
                }
            }
        }
        public void AddPeice(int[,] boardState ,int col, int row)
        {
            Image image;
            Graphics graphic = Graphics.FromImage(boardImage);
            switch (boardState[col, row])
            {
                case 1:
                    image = coin1;
                    break;
                case -1:
                    image = coin2;
                    break;
                default:
                    image = coinDefault;
                    break;
            }
            int x = col * radius + (PAD * col) + PAD;
            int y = row * radius + (PAD * row );
            graphic.DrawImage(image,x,y);
            graphic.Dispose();

            //g.FillEllipse(brush,col * radius + (PAD * col) + PAD, row * radius + (PAD * (row + 1)) + radius,radius, radius);
        }

        private void SetCanvas(object sender, Bitmap img)
        {
            canvas.Image = img;
            this.Invalidate();
        }
        private void SetCanvas(object sender, Board board)
        {
            LoadBoardImg(board.boardState);
            canvas.Image = boardImage;
            this.Invalidate();
        }

        private void canvas_Click(object sender, EventArgs e)
        {            
            OnMoveMade?.Invoke(this,new CanvasClickedArgs(x, y, Properties.Settings.Default.userId));
            //Properties.Settings.Default.userId *= -1;
        }
        private void Winner(object sender, string e)
        {

            canvas.Invoke((MethodInvoker)delegate{canvas.Enabled = false;});
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
        private void menuOptions_Click(object sender, EventArgs e)
        {
            OptionsForm options = new OptionsForm();
            options.OnSettingschange += new EventHandler(handelSettingsChange);
                      
            options.Show();
            initializeImages();
        }

        private void handelSettingsChange(object sender, EventArgs e)
        {
            initializeImages();
        }

        private void menuNewGame_Click(object sender, EventArgs e)
        {
            if (socket.networkActive)
            {
                Random rnd = new Random();
                int turn = rnd.Next(10) > 5 ? 1 : -1;
                GameState newGame = new GameState(turn, new int[COLUMNS, ROWS + 1]);
                socket.Add_send("StartGame" + paramDelimiter + newGame.Get_file());
                startGame(this, newGame);
            }
            else { MessageBox.Show("Please Connect to another Palyer"); }
        }
        private void menuSaveGame_Click(object sender, EventArgs e)
        {
            GameState gameState = engine.GetGameState();
            saveFileDialog.Filter = "Binary File|*.bin";
            saveFileDialog.Title = "Save Game";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                BinaryFileHelper.SaveGameState(saveFileDialog.FileName, gameState);
            }
        }
        private void menuLoadGame_Click(object sender, EventArgs e)
        {
            if (socket.networkActive)
            {
                openFileDialog.Filter = "Binary File|*.bin";
                openFileDialog.Title = "Load Game";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GameState gameState = BinaryFileHelper.LoadGameState(openFileDialog.FileName);
                    socket.Add_send("StartGame" + paramDelimiter + gameState.Get_file());
                    startGame(this, gameState);
                }
            }
            else { MessageBox.Show("Please Connect to another Palyer"); }
        }
        private void startGame(object o, GameState gameState)
        {            
            engine.StartGame(gameState);
            canvas.Invoke((MethodInvoker)delegate { canvas.Enabled = true; });
        }

        // NetWorking Stuff
        private void connectButton_Click(object sender, EventArgs e)
        {
            if (!socket.networkActive)
            {
                if (clientCheckBox.Checked && ipPattern.IsMatch(ipInput.Text))
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
                    MessageBox.Show("Please Enter valid IP Address");
                }
            }
        }
        private void clientCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!socket.networkActive)
            {
                if (clientCheckBox.Checked)
                {
                    connectButton.Text = "Connect";
                    menuNewGame.Enabled = false;
                    menuLoadGame.Enabled = false;
                }
                else
                {
                    menuNewGame.Enabled = true;
                    menuLoadGame.Enabled = true;
                    connectButton.Text = "Host Game";
                    try { socket.SetIp(""); } catch { }
                }                
            }
            else
            {
               // if (clientCheckBox.Checked) { clientCheckBox.Checked = false; }
                //else { clientCheckBox.Checked = true; }
            
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
            Thread.Sleep(100);
            if (thread.IsAlive)
            {
                socket.Dispose();
                //thread.Abort();                
            }
        }


        // System handling
        private void ErrorBox(object sender, string error)
        {
            MessageBox.Show("Error: "+ error);
        }
        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            boardImage.Dispose();
            coin1.Dispose();
            coin2.Dispose();
            coinDefault.Dispose();
            socket.networkActive = false;
            socket.OnError -= new EventHandler<string>(ErrorBox);
            socket.OnChatRecieved -= new EventHandler<string>(chatRecieved);
            socket.OnMoveRecieved -= new EventHandler<MoveArgs>(engine.Move);
            socket.OnNewGame -= new EventHandler<GameState>(startGame);
            socket.OnDisconnect -= new EventHandler(handDisiconnection);
            socket.OnConnected -= new EventHandler(handConnected);

            Thread.Sleep(50);
            socket.Dispose();
            //Thread.Sleep(500);   // wait for network to close connection            

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
        private void SendMsgButton_Click(object sender, EventArgs e)
        {            
            socket.Add_send("Chat"+ paramDelimiter.ToString()+ 
                Properties.Settings.Default.userName + paramDelimiter.ToString() + 
                ":" + messageInput.Text);
            messageInput.Text = "";
        }

        private void ipInput_TextChanged(object sender, EventArgs e)
        {            
            if (!ipPattern.IsMatch(ipInput.Text))
            {
                if (ipInput.Text.Length > 0)
                {
                    ipWarningLabel.Text = "Warning IP address not valid";
                }

            }
            else { ipWarningLabel.Text = ""; }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            initializeImages();
            Random rnd = new Random();
            int n = rnd.Next(0, 7);
            engine.Move(this, new MoveArgs(n, engine.Turn));           
        }

    }
}
