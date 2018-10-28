using System;
using System.Drawing;

namespace ConnectFour
{
    public class UpdateBoardArgs : EventArgs        // define my event args
    {
        public UpdateBoardArgs(Bitmap img) => this.img = img;
        public Bitmap img{ get; set; }
    }

}
