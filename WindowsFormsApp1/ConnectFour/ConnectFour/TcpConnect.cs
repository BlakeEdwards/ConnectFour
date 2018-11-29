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
        private const string commandDelimiter = "~";
        private const char paramDelimiter = '|';
        private bool isServer = false;
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
        public event EventHandler<GameState> OnNewGame;        
        public event EventHandler OnDisconnect;
        public event EventHandler OnConnected;

        /// <summary>
        /// Setup will create a server
        /// To Set as client SetIp be for Starting
        /// </summary>
        /// <param name="port"></param>
        public TcpConnect(int port)
        {
            ip = "";
            sendData = "";
            this.port = port;            
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            // TcpListener server = new TcpListener(port);            
        }
        /// <summary>
        /// Entry Point to start Network and Active Loop
        /// </summary>
        public void Start()
        {
            isServer = ip.Length <= 0 ? true : false;            
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
                    networkActive = true;

                    // Set Id of this Machine
                    Properties.Settings.Default.userId = 1;
                }
                catch(Exception e)
                {
                    OnChatRecieved?.Invoke(this, "Connecting Stoped");
                    try { listener.Stop(); } catch { }
                    //OnError?.Invoke(this, e.Message);
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
                    networkActive = true;
                    Properties.Settings.Default.userId = -1;
                }
                catch(Exception e)
                {
                    OnError?.Invoke(this, e.Message);
                    networkActive = false;
                    otherSideClosing = true;
                }
            }
            if (client != null && networkActive)
            {
                HandShake();
                Network_loop();
            }
            closeConnection();
        }

        private void HandShake()
        {
            sendData = Properties.Settings.Default.userName;
            send();
            Thread.Sleep(30);
            while (client.Available >0)
            {
                byte[] msgBuffer = new byte[client.Available];
                netStream.Read(msgBuffer, 0, client.Available);
                recieveData = Encoding.UTF8.GetString(msgBuffer);
            }

            netStream.Flush();

            Properties.Settings.Default.otherPlayer = recieveData;
            if (OnConnected !=null) OnConnected(this, new EventArgs());
        }

        internal void localMoveMade(object sender, MoveArgs e)
        {

            string cmd = "Move|" + e.playerId + paramDelimiter + e.col;
            Add_send(cmd);
        }

        private void Network_loop()
        {
            if (isServer)
            {

            }
            else
            {

            }
            while (networkActive)
            {
                if (!client.Connected) networkActive = false;
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
        }

        private void send()
        {
            // Todo tcp Send
            //finalize current set of inputs with <EOF>
            byte[] msgBuffer = Encoding.UTF8.GetBytes(sendData);
            netStream.Write(msgBuffer, 0, msgBuffer.Length); // Blocks
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
            if (recieveData.Length > 0) { Execute_Commands(recieveData, "Recieved"); }
            netStream.Flush();
        }
        /// <summary>
        /// TcpConnection interface for sending cmd across the Network
        /// </summary>
        /// <param name="cmd"></param>
        public void Add_send(string cmd)
        {
            // ensure user entered data doesnt contain a command delimiter
            cmd = cmd.Replace(commandDelimiter, "");
            string[] chatcheck = cmd.Split(paramDelimiter);
            // define end of command with ~ delimiter
            
                sendData += cmd + commandDelimiter;
            if (chatcheck[0] == "Chat") { Execute_Commands(cmd, "Sent"); }
        }

        //extract out to seperate class commands
        // net work is just send receive
        private void Execute_Commands(string data, string pretext)
        {
            string[] command = data.Split('~');
            for (int i = 0; i < command.Length; i++)
            {
                string[] cmd = command[i].Split(paramDelimiter);
                switch (cmd[0])
                {// constatnats any litral the data is in the code
                    case "Chat":                                                
                        if (cmd[2].Length > 0)
                        {// send obj not primatives obj validation or some function
                            if (pretext == "Sent") { pretext = "You"; }//Properties.Settings.Default.userName; }
                            else { pretext = cmd[1]; }
                            OnChatRecieved?.Invoke(this, pretext + cmd[2]);
                        }
                        break;
                    case "Move":
                        OnMoveRecieved?.Invoke(this, new MoveArgs( int.Parse(cmd[2]), int.Parse(cmd[1]) ));
                        break;
                    case "StartGame":
                        OnNewGame?.Invoke(this, GameState.GetGameState(cmd[1]));
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
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            OnDisconnect?.Invoke(this, new EventArgs());
            networkActive = false;
            otherSideClosing = false;
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
            try{listener.Stop();}catch (Exception e) { Console.WriteLine(e.ToString()); }
            try {client.Dispose(); } catch (Exception e) { Console.WriteLine(e.ToString()); }
            try {netStream.Dispose(); ; } catch (Exception e) { Console.WriteLine(e.ToString()); }
            
            
            GC.SuppressFinalize(this);

        }
    }
}



