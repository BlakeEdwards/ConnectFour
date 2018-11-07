using System;

namespace ConnectFour
{
    public class MoveArgs : EventArgs        // define my event args
    {
        public MoveArgs(int col,int playerId)
        {
            this.col = col;            
            this.playerId = playerId;
        }
        public int col { get; set; }
        public int playerId { get; set; }
    }

}
