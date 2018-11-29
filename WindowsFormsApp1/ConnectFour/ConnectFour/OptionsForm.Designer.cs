namespace ConnectFour
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.play1Col = new System.Windows.Forms.PictureBox();
            this.bGCol = new System.Windows.Forms.PictureBox();
            this.play2Col = new System.Windows.Forms.PictureBox();
            this.boardCol = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.img1Check = new System.Windows.Forms.CheckBox();
            this.img1PathLabel = new System.Windows.Forms.Label();
            this.selectPath1Button = new System.Windows.Forms.Button();
            this.img2PathLabel = new System.Windows.Forms.Label();
            this.img2Check = new System.Windows.Forms.CheckBox();
            this.selectPath2Button = new System.Windows.Forms.Button();
            this.getPathDialog = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.play1Col)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bGCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.play2Col)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardCol)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player 1 Corour";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(130, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Player 2 Corour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Board Colour";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(396, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "BackGroundCorour";
            // 
            // play1Col
            // 
            this.play1Col.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.play1Col.Location = new System.Drawing.Point(117, 44);
            this.play1Col.Name = "play1Col";
            this.play1Col.Size = new System.Drawing.Size(70, 36);
            this.play1Col.TabIndex = 9;
            this.play1Col.TabStop = false;
            this.play1Col.Click += new System.EventHandler(this.play1Col_Click);
            // 
            // bGCol
            // 
            this.bGCol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bGCol.Location = new System.Drawing.Point(500, 44);
            this.bGCol.Name = "bGCol";
            this.bGCol.Size = new System.Drawing.Size(70, 36);
            this.bGCol.TabIndex = 10;
            this.bGCol.TabStop = false;
            this.bGCol.Click += new System.EventHandler(this.bGCol_Click);
            // 
            // play2Col
            // 
            this.play2Col.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.play2Col.Location = new System.Drawing.Point(117, 144);
            this.play2Col.Name = "play2Col";
            this.play2Col.Size = new System.Drawing.Size(70, 36);
            this.play2Col.TabIndex = 11;
            this.play2Col.TabStop = false;
            this.play2Col.Click += new System.EventHandler(this.play2Col_Click);
            // 
            // boardCol
            // 
            this.boardCol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardCol.Location = new System.Drawing.Point(500, 144);
            this.boardCol.Name = "boardCol";
            this.boardCol.Size = new System.Drawing.Size(70, 36);
            this.boardCol.TabIndex = 12;
            this.boardCol.TabStop = false;
            this.boardCol.Click += new System.EventHandler(this.boardCol_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(607, 184);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(70, 29);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // img1Check
            // 
            this.img1Check.AutoSize = true;
            this.img1Check.Location = new System.Drawing.Point(209, 44);
            this.img1Check.Name = "img1Check";
            this.img1Check.Size = new System.Drawing.Size(131, 17);
            this.img1Check.TabIndex = 14;
            this.img1Check.Text = "Use image for player 1";
            this.img1Check.UseVisualStyleBackColor = true;
            this.img1Check.Click += new System.EventHandler(this.img1Check_Click);
            // 
            // img1PathLabel
            // 
            this.img1PathLabel.AutoSize = true;
            this.img1PathLabel.Location = new System.Drawing.Point(117, 97);
            this.img1PathLabel.Name = "img1PathLabel";
            this.img1PathLabel.Size = new System.Drawing.Size(0, 13);
            this.img1PathLabel.TabIndex = 15;
            // 
            // selectPath1Button
            // 
            this.selectPath1Button.Location = new System.Drawing.Point(209, 67);
            this.selectPath1Button.Name = "selectPath1Button";
            this.selectPath1Button.Size = new System.Drawing.Size(131, 21);
            this.selectPath1Button.TabIndex = 16;
            this.selectPath1Button.Text = "Select Image";
            this.selectPath1Button.UseVisualStyleBackColor = true;
            this.selectPath1Button.Click += new System.EventHandler(this.selectPath1Button_Click);
            // 
            // img2PathLabel
            // 
            this.img2PathLabel.AutoSize = true;
            this.img2PathLabel.Location = new System.Drawing.Point(117, 200);
            this.img2PathLabel.Name = "img2PathLabel";
            this.img2PathLabel.Size = new System.Drawing.Size(0, 13);
            this.img2PathLabel.TabIndex = 18;
            // 
            // img2Check
            // 
            this.img2Check.AutoSize = true;
            this.img2Check.Location = new System.Drawing.Point(209, 147);
            this.img2Check.Name = "img2Check";
            this.img2Check.Size = new System.Drawing.Size(131, 17);
            this.img2Check.TabIndex = 17;
            this.img2Check.Text = "Use image for player 1";
            this.img2Check.UseVisualStyleBackColor = true;
            this.img2Check.CheckedChanged += new System.EventHandler(this.img2Check_CheckedChanged);
            // 
            // selectPath2Button
            // 
            this.selectPath2Button.Location = new System.Drawing.Point(209, 170);
            this.selectPath2Button.Name = "selectPath2Button";
            this.selectPath2Button.Size = new System.Drawing.Size(131, 21);
            this.selectPath2Button.TabIndex = 19;
            this.selectPath2Button.Text = "Select Image";
            this.selectPath2Button.UseVisualStyleBackColor = true;
            this.selectPath2Button.Click += new System.EventHandler(this.selectPath2Button_Click);
            // 
            // getPathDialog
            // 
            this.getPathDialog.FileName = "openFileDialog1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Image file path";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Image file path";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 226);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.selectPath2Button);
            this.Controls.Add(this.img2PathLabel);
            this.Controls.Add(this.img2Check);
            this.Controls.Add(this.selectPath1Button);
            this.Controls.Add(this.img1PathLabel);
            this.Controls.Add(this.img1Check);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.boardCol);
            this.Controls.Add(this.play2Col);
            this.Controls.Add(this.bGCol);
            this.Controls.Add(this.play1Col);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.Text = "Custom Settings";
            ((System.ComponentModel.ISupportInitialize)(this.play1Col)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bGCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.play2Col)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardCol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox play1Col;
        private System.Windows.Forms.PictureBox bGCol;
        private System.Windows.Forms.PictureBox play2Col;
        private System.Windows.Forms.PictureBox boardCol;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox img1Check;
        private System.Windows.Forms.Label img1PathLabel;
        private System.Windows.Forms.Button selectPath1Button;
        private System.Windows.Forms.Label img2PathLabel;
        private System.Windows.Forms.CheckBox img2Check;
        private System.Windows.Forms.Button selectPath2Button;
        private System.Windows.Forms.OpenFileDialog getPathDialog;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}