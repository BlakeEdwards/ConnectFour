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
            this.button1 = new System.Windows.Forms.Button();
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
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Player 2 Corour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Board Colour";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(302, 56);
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
            this.bGCol.Location = new System.Drawing.Point(406, 44);
            this.bGCol.Name = "bGCol";
            this.bGCol.Size = new System.Drawing.Size(70, 36);
            this.bGCol.TabIndex = 10;
            this.bGCol.TabStop = false;
            this.bGCol.Click += new System.EventHandler(this.bGCol_Click);
            // 
            // play2Col
            // 
            this.play2Col.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.play2Col.Location = new System.Drawing.Point(116, 97);
            this.play2Col.Name = "play2Col";
            this.play2Col.Size = new System.Drawing.Size(70, 36);
            this.play2Col.TabIndex = 11;
            this.play2Col.TabStop = false;
            this.play2Col.Click += new System.EventHandler(this.play2Col_Click);
            // 
            // boardCol
            // 
            this.boardCol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boardCol.Location = new System.Drawing.Point(406, 97);
            this.boardCol.Name = "boardCol";
            this.boardCol.Size = new System.Drawing.Size(70, 36);
            this.boardCol.TabIndex = 12;
            this.boardCol.TabStop = false;
            this.boardCol.Click += new System.EventHandler(this.boardCol_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(529, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 29);
            this.button1.TabIndex = 13;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 192);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
    }
}