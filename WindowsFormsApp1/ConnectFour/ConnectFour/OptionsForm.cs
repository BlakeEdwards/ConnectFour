using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.userName;
            play1Col.BackColor = Properties.Settings.Default.play1Col;
            play2Col.BackColor = Properties.Settings.Default.play2Col;
            bGCol.BackColor = Properties.Settings.Default.backGroundColor;
            boardCol.BackColor = Properties.Settings.Default.boardColor;
        }

       

        private void play1Col_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                play1Col.BackColor = colorDialog1.Color;
                Properties.Settings.Default.play1Col = colorDialog1.Color;
            }
        }

        private void play2Col_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                play2Col.BackColor = colorDialog1.Color;
                Properties.Settings.Default.play2Col = colorDialog1.Color;
            }
        }

        private void bGCol_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                bGCol.BackColor = colorDialog1.Color;
                Properties.Settings.Default.backGroundColor = colorDialog1.Color;
            }
        }

        private void boardCol_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                boardCol.BackColor = colorDialog1.Color;
                Properties.Settings.Default.boardColor = colorDialog1.Color;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.userName = textBox1.Text;
        }
    }
}
