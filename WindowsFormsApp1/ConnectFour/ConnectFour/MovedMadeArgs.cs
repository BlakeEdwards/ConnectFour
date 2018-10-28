using System;

namespace ConnectFour
{
    public class MovedMadeArgs : EventArgs        // define my event args
    {
        public MovedMadeArgs(int col,int playerId)
        {
            this.col = col;
            this.playerId = playerId;
        }
        public int col { get; set; }
        public int playerId { get; set; }
    }

}
