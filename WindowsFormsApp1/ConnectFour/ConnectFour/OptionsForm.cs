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
        public event EventHandler OnSettingschange;
        private string userName;
        private Color oplay1Col, oplay2Col, obGCol, oboardCol;
        public OptionsForm()
        {
            InitializeComponent();
            //set Local variables
            userName = Properties.Settings.Default.userName;
            oplay1Col= Properties.Settings.Default.play1Col;
            oplay2Col= Properties.Settings.Default.play2Col;
            obGCol= Properties.Settings.Default.backGroundColor;
            oboardCol= Properties.Settings.Default.boardColor;

            // set FormView
            textBox1.Text  = Properties.Settings.Default.userName;
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
                oplay1Col = colorDialog1.Color;
            }
        }

        private void play2Col_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                play2Col.BackColor = colorDialog1.Color;
                oplay2Col = colorDialog1.Color;
            }
        }

        private void bGCol_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                bGCol.BackColor = colorDialog1.Color;
                obGCol = colorDialog1.Color;
            }
        }

        private void img1Check_Click(object sender, EventArgs e)
        {
            if (img1Check.Checked && img1PathLabel.Text == "") img1PathLabel.Text = getPath(); 
        }
        private void img2Check_CheckedChanged(object sender, EventArgs e)
        {
            if (img2Check.Checked && img2PathLabel.Text == "") img2PathLabel.Text = getPath();
        }
        
        private void selectPath1Button_Click(object sender, EventArgs e)
        {
            img1Check.Checked = true;
            img1PathLabel.Text = getPath();
        }
        private void selectPath2Button_Click(object sender, EventArgs e)
        {
            img2Check.Checked = true;            
            img2PathLabel.Text = getPath(); 
        }

        private string getPath()
        {
            getPathDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (getPathDialog.ShowDialog() == DialogResult.OK)
            {
                return getPathDialog.FileName;
            }
                return "";
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            colorDialog1.Dispose();
            getPathDialog.Dispose();
        }

        private void boardCol_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                boardCol.BackColor = colorDialog1.Color;
                oboardCol = colorDialog1.Color;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            userName = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.play1Col = oplay1Col;
            Properties.Settings.Default.play2Col = oplay2Col;
            Properties.Settings.Default.backGroundColor = obGCol;
            Properties.Settings.Default.boardColor = oboardCol;
            Properties.Settings.Default.userName = userName;
            Properties.Settings.Default.play1ImagePath = img1PathLabel.Text;
            Properties.Settings.Default.play2ImagePath = img2PathLabel.Text;
            Properties.Settings.Default.useImg1 =img1Check.Checked ;
            Properties.Settings.Default.useImg2 = img2Check.Checked;
            OnSettingschange?.Invoke(this, new EventArgs());
            this.Close();
        }
    }
}
