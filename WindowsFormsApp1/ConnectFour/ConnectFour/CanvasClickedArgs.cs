using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class CanvasClickedArgs : EventArgs
    {
        public CanvasClickedArgs(int x, int y, int playerId)
        {
            this.x = x;
            this.y = y;
            this.playerId = playerId;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int playerId { get; set; }
    }
}
