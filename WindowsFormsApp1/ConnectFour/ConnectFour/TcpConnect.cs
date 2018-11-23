using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConnectFour
{

    class TcpConnect : IDisposable
    {
        private int port;
        private string ip;
        private bool isServer;
        private bool otherSideClosing = false;
        private TcpClient client;
        private TcpListener listener;
        private string recieveData, sendData;
        NetworkStream netStream;
        byte[] bytes = new Byte[1024];
        public bool networkActive { get; set; }
        public event EventHandler<MoveArgs> OnMoveRecieved;
        public event EventHandler<string> OnChatRecieved;
        public event EventHandler<string> OnError;
        public event EventHandler<int> OnNewGame;
        public event EventHandler<int> OnLoadGame;        
        public event EventHandler OnDisconnect;

        /// <summary>
        /// Setup will create a server
        /// To Set as client SetIp be for Starting
        /// </summary>
        /// <param name="port"></param>
        public TcpConnect(int port)
        {
            sendData = "";
            this.port = port;
            isServer = true;
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            // TcpListener server = new TcpListener(port);            
        }
        /// <summary>
        /// Entry Point to start Network and Active Loop
        /// </summary>
        public void Start()
        {
            
            networkActive = true;
            if (isServer)
            {
                try
                {
                    OnChatRecieved?.Invoke(this, "Waiting client to Connect");
                    // Server Machine
                    // Id = 1
                    listener = new TcpListener(IPAddress.Any, port);
                    listener.Start();
                    client = listener.AcceptTcpClient();
                    OnChatRecieved?.Invoke(this, "server Created");
                    netStream = client.GetStream();
                    listener.Stop();
                    
                    // Set Id of this Machine
                    Properties.Settings.Default.userId = 1;
                }
                catch(Exception e)
                {
                    OnChatRecieved?.Invoke(this, "Connecting Stoped");
                    try { listener.Stop(); } catch { }
                    OnError?.Invoke(this, e.Message);
                    networkActive = false;
                    otherSideClosing = true;
                }
            }
            else
            {
                try
                {
                    // Client Machine
                    // Id = 1
                    client = new TcpClient(ip, port);
                    netStream = client.GetStream();
                    OnChatRecieved?.Invoke(this, "Client Created");
                    netStream = client.GetStream();
                    // Set Id of this Machine
                    Properties.Settings.Default.userId = -1;
                }
                catch(Exception e)
                {
                    OnError?.Invoke(this, e.Message);
                    networkActive = false;
                    otherSideClosing = true;
                }
            }            
            Network_loop();
            closeConnection();
        }

        internal void localMoveMade(object sender, MoveArgs e)
        {

            string cmd = "Move|" + e.playerId + "|" + e.col;
            Add_send(cmd);
        }

        private void Network_loop()
        {
            if (networkActive)
            {
                Random rnd = new Random();
                int turn = rnd.Next( 2);
                // if turn is 1 we start
                // if turn is 0 they start
            }
            while (networkActive)
            {
                if (client != null)
                {
                    lock (client)
                    {
                        if (client.Available > 0)
                        {
                            recieve();
                        }
                        if (sendData.Length > 0)
                        {
                            send();
                        }
                    }
                }
                else
                {
                    networkActive = false;
                }
                Thread.Sleep(10);
            }
            OnChatRecieved?.Invoke(this, "Disconnecting");
        }

        private void send()
        {
            // Todo tcp Send
            //finalize current set of inputs with <EOF>
            byte[] msgBuffer = Encoding.UTF8.GetBytes(sendData);
            netStream.Write(msgBuffer, 0, msgBuffer.Length); // Blocks
            netStream.Flush();
            // clear the data that has been send
            sendData = "";
        }
        private void recieve()
        {
            recieveData = "";
            // todo Tcp Recieve
            byte[] msgBuffer = new byte[client.Available];
            netStream.Read(msgBuffer, 0, client.Available);
            recieveData = Encoding.UTF8.GetString(msgBuffer);
            if (recieveData.Length > 0) { Execute_Commands(recieveData, "Recieved: "); }
        }
        /// <summary>
        /// TcpConnection interface for sending cmd across the Network
        /// </summary>
        /// <param name="cmd"></param>
        public void Add_send(string cmd)
        {
            // ensure user entered data doesnt contain a command delimiter
            cmd = cmd.Replace("~", "");
            string[] chatcheck = cmd.Split('|');
            // define end of command with ~ delimiter
            
                sendData += cmd + "~";
                if (chatcheck[0] == "Chat") { Execute_Commands(cmd, "Sent: "); }
        }
        //extract out to seperate class commands
        // net work is just send receive
        private void Execute_Commands(string data, string pretext)
        {

            // command format cmd•parms /• = alt 7
            //  chat•Hello
            //  move•5
            //  setTurn•1
            //  LoadBoard•board[] ... pending to add Todo
            //  Disconnect•1             // disconect 1 = true : 0 = false
            // chat|bob|hello world ~ move | 7
           
            // command [0] = chat|bob|hello world 
            // command [1] =  move | 7
            string[] command = data.Split('~');
            for (int i = 0; i < command.Length; i++)
            {
                string[] cmd = command[i].Split('|');
                switch (cmd[0])
                {// constatnats any litral the data is in the code
                    case "Chat":
                        if (cmd[1].Length > 0)
                        {// send obj not primatives obj validation or some function
                            OnChatRecieved?.Invoke(this, pretext + cmd[1]);
                        }
                        break;
                    case "Move":
                        OnMoveRecieved?.Invoke(this, new MoveArgs( int.Parse(cmd[2]), int.Parse(cmd[1]) ));
                        break;
                    case "StartNewGame":
                        OnNewGame?.Invoke(this, int.Parse(cmd[1]));
                            break;
                    case "Close":
                        networkActive = false;
                        otherSideClosing = true;
                        break;
                }
            }
        }   
        /// <summary>
        /// When the Network is done call CloseConnection To clean up
        /// reasources
        /// </summary>
        public void closeConnection()
        {
            if (!otherSideClosing)
            {
                sendData = "Close|true";
                send();
            }
            try
            {
                OnChatRecieved?.Invoke(this, "Connection Closed");
                netStream?.Close();
                netStream = null;
                client.Close();
                client.Dispose();                
            }
            catch { }
            OnDisconnect?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// IP can Only be set when network Is Not Running
        /// Setting an IP will make this machine a Client
        /// </summary>
        /// <param name="ip">IP address</param>
        /// <example>SetIp("127.0.0.1")</example>
        public void SetIp(string ip)
        {
            if (!networkActive)
            {
                this.ip = ip;
                isServer = ip == "" ? true : false;
            }
            else
            {
                OnError(this, "Network is Currently Running");
            }
        }

        public void Dispose()
        {
            try
            {
                listener.Stop();
                client.Dispose();                
                netStream.Dispose();
            }
            catch (Exception e){ }
            GC.SuppressFinalize(this);

        }
    }
}



