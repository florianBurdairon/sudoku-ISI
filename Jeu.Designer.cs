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
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.listLeaderboard = new System.Windows.Forms.ListBox();
            this.rbtnEasy = new System.Windows.Forms.RadioButton();
            this.rbtnMedium = new System.Windows.Forms.RadioButton();
            this.rbtnHard = new System.Windows.Forms.RadioButton();
            this.groupRBtn = new System.Windows.Forms.GroupBox();
            this.lbLeftNumbers = new System.Windows.Forms.ListBox();
            this.groupLeftNumbers = new System.Windows.Forms.GroupBox();
            this.gbLeaderboard = new System.Windows.Forms.GroupBox();
            this.gbNotes = new System.Windows.Forms.GroupBox();
            this.cbBlueLines = new System.Windows.Forms.CheckBox();
            clbNote = new System.Windows.Forms.CheckedListBox();
            this.panelGrille.SuspendLayout();
            this.groupRBtn.SuspendLayout();
            this.groupLeftNumbers.SuspendLayout();
            this.gbLeaderboard.SuspendLayout();
            this.gbNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGrille
            // 
            this.panelGrille.Controls.Add(this.btnStart);
            this.panelGrille.Location = new System.Drawing.Point(223, 38);
            this.panelGrille.Name = "panelGrille";
            this.panelGrille.Size = new System.Drawing.Size(450, 450);
            this.panelGrille.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(56, 183);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(128, 55);
            this.btnStart.TabIndex = 2;
            this.btnStart.TabStop = false;
            this.btnStart.Text = "Nouvelle partie";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(505, 494);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(110, 38);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.TabStop = false;
            this.btnHelp.Text = "Aide";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Visible = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(279, 494);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(110, 38);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.TabStop = false;
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
            this.lbTime.Location = new System.Drawing.Point(397, 541);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(98, 20);
            this.lbTime.TabIndex = 5;
            this.lbTime.Text = "Temps : 02:51";
            this.lbTime.Visible = false;
            // 
            // listLeaderboard
            // 
            this.listLeaderboard.BackColor = System.Drawing.SystemColors.Control;
            this.listLeaderboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listLeaderboard.FormattingEnabled = true;
            this.listLeaderboard.ItemHeight = 20;
            this.listLeaderboard.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.listLeaderboard.Location = new System.Drawing.Point(9, 23);
            this.listLeaderboard.Name = "listLeaderboard";
            this.listLeaderboard.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listLeaderboard.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listLeaderboard.Size = new System.Drawing.Size(204, 420);
            this.listLeaderboard.TabIndex = 6;
            this.listLeaderboard.TabStop = false;
            // 
            // rbtnEasy
            // 
            this.rbtnEasy.AutoSize = true;
            this.rbtnEasy.Location = new System.Drawing.Point(48, 51);
            this.rbtnEasy.Name = "rbtnEasy";
            this.rbtnEasy.Size = new System.Drawing.Size(67, 24);
            this.rbtnEasy.TabIndex = 7;
            this.rbtnEasy.Text = "Facile";
            this.rbtnEasy.UseVisualStyleBackColor = true;
            // 
            // rbtnMedium
            // 
            this.rbtnMedium.AutoSize = true;
            this.rbtnMedium.Checked = true;
            this.rbtnMedium.Location = new System.Drawing.Point(45, 81);
            this.rbtnMedium.Name = "rbtnMedium";
            this.rbtnMedium.Size = new System.Drawing.Size(75, 24);
            this.rbtnMedium.TabIndex = 8;
            this.rbtnMedium.TabStop = true;
            this.rbtnMedium.Text = "Moyen";
            this.rbtnMedium.UseVisualStyleBackColor = true;
            // 
            // rbtnHard
            // 
            this.rbtnHard.AutoSize = true;
            this.rbtnHard.Location = new System.Drawing.Point(42, 111);
            this.rbtnHard.Name = "rbtnHard";
            this.rbtnHard.Size = new System.Drawing.Size(82, 24);
            this.rbtnHard.TabIndex = 9;
            this.rbtnHard.Text = "Difficile";
            this.rbtnHard.UseVisualStyleBackColor = true;
            // 
            // groupRBtn
            // 
            this.groupRBtn.Controls.Add(this.rbtnEasy);
            this.groupRBtn.Controls.Add(this.rbtnHard);
            this.groupRBtn.Controls.Add(this.rbtnMedium);
            this.groupRBtn.Location = new System.Drawing.Point(22, 347);
            this.groupRBtn.Name = "groupRBtn";
            this.groupRBtn.Size = new System.Drawing.Size(169, 141);
            this.groupRBtn.TabIndex = 10;
            this.groupRBtn.TabStop = false;
            this.groupRBtn.Text = "Choix de la difficulté lors de la génération : ";
            // 
            // lbLeftNumbers
            // 
            this.lbLeftNumbers.BackColor = System.Drawing.SystemColors.Control;
            this.lbLeftNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbLeftNumbers.FormattingEnabled = true;
            this.lbLeftNumbers.ItemHeight = 20;
            this.lbLeftNumbers.Items.AddRange(new object[] {
            "1 : ",
            "2 :",
            "3 : ",
            "4 :",
            "5 :",
            "6 :",
            "7 :",
            "8 :",
            "9 :"});
            this.lbLeftNumbers.Location = new System.Drawing.Point(9, 45);
            this.lbLeftNumbers.Name = "lbLeftNumbers";
            this.lbLeftNumbers.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbLeftNumbers.Size = new System.Drawing.Size(77, 180);
            this.lbLeftNumbers.TabIndex = 12;
            this.lbLeftNumbers.TabStop = false;
            // 
            // groupLeftNumbers
            // 
            this.groupLeftNumbers.BackColor = System.Drawing.SystemColors.Control;
            this.groupLeftNumbers.Controls.Add(this.lbLeftNumbers);
            this.groupLeftNumbers.Location = new System.Drawing.Point(17, 38);
            this.groupLeftNumbers.Name = "groupLeftNumbers";
            this.groupLeftNumbers.Size = new System.Drawing.Size(88, 238);
            this.groupLeftNumbers.TabIndex = 13;
            this.groupLeftNumbers.TabStop = false;
            this.groupLeftNumbers.Text = "Nombres restants :";
            // 
            // gbLeaderboard
            // 
            this.gbLeaderboard.Controls.Add(this.listLeaderboard);
            this.gbLeaderboard.Location = new System.Drawing.Point(683, 38);
            this.gbLeaderboard.Name = "gbLeaderboard";
            this.gbLeaderboard.Size = new System.Drawing.Size(222, 450);
            this.gbLeaderboard.TabIndex = 14;
            this.gbLeaderboard.TabStop = false;
            this.gbLeaderboard.Text = "Tableau des scores (Difficile) :";
            // 
            // gbNotes
            // 
            this.gbNotes.Controls.Add(clbNote);
            this.gbNotes.Location = new System.Drawing.Point(105, 38);
            this.gbNotes.Name = "gbNotes";
            this.gbNotes.Size = new System.Drawing.Size(110, 238);
            this.gbNotes.TabIndex = 15;
            this.gbNotes.TabStop = false;
            this.gbNotes.Text = "Annotations : ";
            this.gbNotes.Visible = false;
            // 
            // clbNote
            // 
            clbNote.BackColor = System.Drawing.SystemColors.Control;
            clbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            clbNote.CheckOnClick = true;
            clbNote.FormattingEnabled = true;
            clbNote.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            clbNote.Location = new System.Drawing.Point(37, 23);
            clbNote.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            clbNote.Name = "clbNote";
            clbNote.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            clbNote.Size = new System.Drawing.Size(49, 198);
            clbNote.Sorted = true;
            clbNote.TabIndex = 1;
            clbNote.TabStop = false;
            clbNote.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbNotes_ItemCheck);
            // 
            // cbBlueLines
            // 
            this.cbBlueLines.AutoSize = true;
            this.cbBlueLines.Checked = true;
            this.cbBlueLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBlueLines.Location = new System.Drawing.Point(22, 303);
            this.cbBlueLines.Name = "cbBlueLines";
            this.cbBlueLines.Size = new System.Drawing.Size(187, 24);
            this.cbBlueLines.TabIndex = 16;
            this.cbBlueLines.TabStop = false;
            this.cbBlueLines.Text = "Retirer les lignes bleues";
            this.cbBlueLines.UseVisualStyleBackColor = true;
            this.cbBlueLines.CheckedChanged += new System.EventHandler(this.cbBlueLines_CheckedChanged);
            // 
            // Jeu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(921, 583);
            this.Controls.Add(this.cbBlueLines);
            this.Controls.Add(this.gbNotes);
            this.Controls.Add(this.gbLeaderboard);
            this.Controls.Add(this.groupLeftNumbers);
            this.Controls.Add(this.groupRBtn);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.panelGrille);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(939, 630);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(939, 630);
            this.Name = "Jeu";
            this.Text = "Sudoku";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Jeu_FormClosed);
            this.panelGrille.ResumeLayout(false);
            this.groupRBtn.ResumeLayout(false);
            this.groupRBtn.PerformLayout();
            this.groupLeftNumbers.ResumeLayout(false);
            this.gbLeaderboard.ResumeLayout(false);
            this.gbNotes.ResumeLayout(false);
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
        private GroupBox groupRBtn;
        private RadioButton rbtnEasy;
        private RadioButton rbtnMedium;
        private RadioButton rbtnHard;
        private ListBox lbLeftNumbers;
        private GroupBox groupLeftNumbers;
        private GroupBox gbLeaderboard;
        private GroupBox gbNotes;
        private CheckBox cbBlueLines;
    }
}