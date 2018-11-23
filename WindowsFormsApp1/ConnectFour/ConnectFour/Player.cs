using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Player
    {
        public static int Radius { get; set; }
        public static int Count { get; set; }
        public string Name { get; set; }
        public Bitmap Coin { get; private set; }
        public Color coinColor { get; set; }
        public Player(string name, Color color)
        {
            this.Name = name;
            this.coinColor = color;
            Generate_Coin(this.coinColor);
            Count++;
        }


        private void Generate_Coin(Color coinColor)
        {
            //throw new NotImplementedException();
        }
    }
}
