using System;

namespace ConnectFour
{
    partial class Form1 
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageInput = new System.Windows.Forms.TextBox();
            this.ChatScreen = new System.Windows.Forms.RichTextBox();
            this.aiCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.ipInput = new System.Windows.Forms.TextBox();
            this.clientCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.disconectButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.testButton = new System.Windows.Forms.Button();
            this.SendMsgButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ipWarningLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ControlLight;
            this.canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(12, 54);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(600, 600);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.menuOptions,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(905, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewGame,
            this.menuSaveGame,
            this.menuLoadGame});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // menuNewGame
            // 
            this.menuNewGame.Name = "menuNewGame";
            this.menuNewGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuNewGame.Size = new System.Drawing.Size(141, 22);
            this.menuNewGame.Text = "New";
            this.menuNewGame.Click += new System.EventHandler(this.menuNewGame_Click);
            // 
            // menuSaveGame
            // 
            this.menuSaveGame.Name = "menuSaveGame";
            this.menuSaveGame.Size = new System.Drawing.Size(141, 22);
            this.menuSaveGame.Text = "Save Game";
            this.menuSaveGame.Click += new System.EventHandler(this.menuSaveGame_Click);
            // 
            // menuLoadGame
            // 
            this.menuLoadGame.Name = "menuLoadGame";
            this.menuLoadGame.Size = new System.Drawing.Size(141, 22);
            this.menuLoadGame.Text = "Load Game";
            this.menuLoadGame.Click += new System.EventHandler(this.menuLoadGame_Click);
            // 
            // menuOptions
            // 
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(61, 20);
            this.menuOptions.Text = "Options";
            this.menuOptions.Click += new System.EventHandler(this.menuOptions_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // messageInput
            // 
            this.messageInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.messageInput.Location = new System.Drawing.Point(654, 595);
            this.messageInput.Name = "messageInput";
            this.messageInput.Size = new System.Drawing.Size(224, 20);
            this.messageInput.TabIndex = 2;
            // 
            // ChatScreen
            // 
            this.ChatScreen.BackColor = System.Drawing.Color.White;
            this.ChatScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChatScreen.HideSelection = false;
            this.ChatScreen.Location = new System.Drawing.Point(653, 278);
            this.ChatScreen.Name = "ChatScreen";
            this.ChatScreen.ReadOnly = true;
            this.ChatScreen.Size = new System.Drawing.Size(223, 300);
            this.ChatScreen.TabIndex = 3;
            this.ChatScreen.TabStop = false;
            this.ChatScreen.Text = "";
            // 
            // aiCheckBox
            // 
            this.aiCheckBox.AutoSize = true;
            this.aiCheckBox.Location = new System.Drawing.Point(12, 27);
            this.aiCheckBox.Name = "aiCheckBox";
            this.aiCheckBox.Size = new System.Drawing.Size(68, 17);
            this.aiCheckBox.TabIndex = 4;
            this.aiCheckBox.Text = "AI Player";
            this.aiCheckBox.UseVisualStyleBackColor = true;
            this.aiCheckBox.CheckedChanged += new System.EventHandler(this.aiCheckBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(654, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Reset Board";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ResetBoard);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(641, 54);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 7;
            this.connectButton.Text = "Host Game";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // ipInput
            // 
            this.ipInput.Location = new System.Drawing.Point(641, 142);
            this.ipInput.Name = "ipInput";
            this.ipInput.Size = new System.Drawing.Size(100, 20);
            this.ipInput.TabIndex = 8;
            this.ipInput.Text = "127.0.0.1";
            this.ipInput.TextChanged += new System.EventHandler(this.ipInput_TextChanged);
            // 
            // clientCheckBox
            // 
            this.clientCheckBox.AutoSize = true;
            this.clientCheckBox.Location = new System.Drawing.Point(641, 92);
            this.clientCheckBox.Name = "clientCheckBox";
            this.clientCheckBox.Size = new System.Drawing.Size(132, 17);
            this.clientCheckBox.TabIndex = 9;
            this.clientCheckBox.Text = "Connect to IP Address";
            this.clientCheckBox.UseVisualStyleBackColor = true;
            this.clientCheckBox.CheckedChanged += new System.EventHandler(this.clientCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "IP Address";
            // 
            // disconectButton
            // 
            this.disconectButton.Location = new System.Drawing.Point(775, 54);
            this.disconectButton.Name = "disconectButton";
            this.disconectButton.Size = new System.Drawing.Size(75, 23);
            this.disconectButton.TabIndex = 11;
            this.disconectButton.Text = "Disconect";
            this.disconectButton.UseVisualStyleBackColor = true;
            this.disconectButton.Click += new System.EventHandler(this.disconectButton_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(802, 139);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 12;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // SendMsgButton
            // 
            this.SendMsgButton.Location = new System.Drawing.Point(652, 626);
            this.SendMsgButton.Name = "SendMsgButton";
            this.SendMsgButton.Size = new System.Drawing.Size(224, 23);
            this.SendMsgButton.TabIndex = 13;
            this.SendMsgButton.Text = "Send Message";
            this.SendMsgButton.UseVisualStyleBackColor = true;
            this.SendMsgButton.Click += new System.EventHandler(this.SendMsgButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // ipWarningLabel
            // 
            this.ipWarningLabel.AutoSize = true;
            this.ipWarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.ipWarningLabel.Location = new System.Drawing.Point(641, 169);
            this.ipWarningLabel.Name = "ipWarningLabel";
            this.ipWarningLabel.Size = new System.Drawing.Size(0, 16);
            this.ipWarningLabel.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 661);
            this.Controls.Add(this.ipWarningLabel);
            this.Controls.Add(this.SendMsgButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.disconectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clientCheckBox);
            this.Controls.Add(this.ipInput);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.aiCheckBox);
            this.Controls.Add(this.ChatScreen);
            this.Controls.Add(this.messageInput);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(921, 700);
            this.MinimumSize = new System.Drawing.Size(921, 700);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewGame;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TextBox messageInput;
        private System.Windows.Forms.RichTextBox ChatScreen;
        private System.Windows.Forms.CheckBox aiCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox ipInput;
        private System.Windows.Forms.CheckBox clientCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button disconectButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button SendMsgButton;
        private System.Windows.Forms.ToolStripMenuItem menuSaveGame;
        private System.Windows.Forms.ToolStripMenuItem menuLoadGame;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label ipWarningLabel;
    }
}

