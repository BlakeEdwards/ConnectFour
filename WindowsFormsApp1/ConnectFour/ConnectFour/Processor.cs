using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Processor
    {
        //private TcpConnect Connection { get; set; }
        //public bool IsServer { get { return Connection.IsServer; } }
        //private bool otherSideClosing = false;
        //private string recieveData = "";
/*
        public event EventHandler<string> OnChatRecieved;
        public event EventHandler<string> OnError;
        public event EventHandler<int> OnGameStartRequest;
        public event EventHandler<int> OnLoadGame;
        public event EventHandler<string> OnMessageRecieved;
        public event EventHandler<MoveArgs> OnMoveRecieved;

        private const string commandDelimiter = "~";
        private const char paramDelimiter = '|';

        public Processor()
        {
        
            // create Events To triger the events for each Unit network form and engine

            // ToDo  Plugin Socket Network Events That the processor will be listening to do something
            /*

            //-----------------------

            // ToDo  Plugin Engine Events That the processor will be listening to do something
            // engine Subsciptions
            /*
            //---------------------
        }
        public void startConnection(string ip ,int port)
        {
            //port 4443
            Connection = new TcpConnect(port);
            Connection.SetIp(ip);
            Connection.Start();
        }
        //Engine Handling
        // Start Game/Load      MakeMove            /Win check  /Set Ai
        //GameState             Column &PlayerId    Bool        Bool
        //Todo                  Todo                Todo        Todo

        //Form Handling
        // Send msg     /Make Move /  Hover      /Change Settings
        //msg           Column PlayerId         Settings
        //Todo                  Todo                Todo

        // Network Handling
        // Recieve network Commands     /Send Commands
        // Todo                          Todo
        public void send(object o, string msg)
        {
            Connection.Add_send(msg);
            string[] chatcheck = msg.Split(paramDelimiter);
            if (chatcheck[0] == "Chat") { Execute_Commands(msg, "Sent"); }

            if (recieveData.Length > 0) { Execute_Commands(recieveData, "Recieved"); }
        }

        private void Execute_Commands(string data, string pretext)
        {
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
                        OnMoveRecieved?.Invoke(this, new MoveArgs(int.Parse(cmd[2]), int.Parse(cmd[1])));
                        break;
                    case "StartNewGame":
                        //OnNewGame?.Invoke(this, int.Parse(cmd[1]));
                        break;
                    case "Close":
                        Connection.networkActive = false;
                        Connection.other= true;
                        break;
                }
            }
        }
        


    */
    }

}
