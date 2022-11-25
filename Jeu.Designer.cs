namespace sudoku
{
    partial class Jeu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelGrille = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.clbNote = new System.Windows.Forms.CheckedListBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.listLeaderboard = new System.Windows.Forms.ListBox();
            this.panelGrille.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGrille
            // 
            this.panelGrille.Controls.Add(this.btnStart);
            this.panelGrille.Location = new System.Drawing.Point(206, 38);
            this.panelGrille.Name = "panelGrille";
            this.panelGrille.Size = new System.Drawing.Size(450, 450);
            this.panelGrille.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(101, 253);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(128, 29);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Nouvelle partie";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // clbNote
            // 
            this.clbNote.BackColor = System.Drawing.SystemColors.Control;
            this.clbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbNote.CheckOnClick = true;
            this.clbNote.FormattingEnabled = true;
            this.clbNote.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.clbNote.Location = new System.Drawing.Point(84, 122);
            this.clbNote.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clbNote.Name = "clbNote";
            this.clbNote.Size = new System.Drawing.Size(49, 198);
            this.clbNote.Sorted = true;
            this.clbNote.TabIndex = 1;
            this.clbNote.TabStop = false;
            this.clbNote.Visible = false;
            this.clbNote.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbNotes_ItemCheck);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(483, 494);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(110, 29);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "Aide";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Visible = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(242, 494);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(110, 29);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.Text = "Redémarrer";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Visible = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(393, 539);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(42, 15);
            this.lbTime.TabIndex = 5;
            this.lbTime.Text = "Time : ";
            this.lbTime.Visible = false;
            // 
            // listLeaderboard
            // 
            this.listLeaderboard.BackColor = System.Drawing.SystemColors.Control;
            this.listLeaderboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listLeaderboard.FormattingEnabled = true;
            this.listLeaderboard.ItemHeight = 15;
            this.listLeaderboard.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.listLeaderboard.Location = new System.Drawing.Point(718, 38);
            this.listLeaderboard.Name = "listLeaderboard";
            this.listLeaderboard.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listLeaderboard.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listLeaderboard.Size = new System.Drawing.Size(150, 435);
            this.listLeaderboard.TabIndex = 6;
            this.listLeaderboard.TabStop = false;
            // 
            // Jeu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(921, 595);
            this.Controls.Add(this.listLeaderboard);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.clbNote);
            this.Controls.Add(this.panelGrille);
            this.Name = "Jeu";
            this.Text = "Sudoku";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Jeu_FormClosed);
            this.panelGrille.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panelGrille;
        private CheckedListBox clbNote;
        private Button btnStart;
        private Button btnHelp;
        private Button btnRestart;
        private System.Windows.Forms.Timer timer;
        private Label lbTime;
        private ListBox listLeaderboard;
        private Button btnContinue = null;
    }
}