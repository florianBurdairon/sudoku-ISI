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
            this.panelGrille = new System.Windows.Forms.Panel();
            this.clbNote = new System.Windows.Forms.CheckedListBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelGrille
            // 
            this.panelGrille.Location = new System.Drawing.Point(206, 38);
            this.panelGrille.Name = "panelGrille";
            this.panelGrille.Size = new System.Drawing.Size(450, 450);
            this.panelGrille.TabIndex = 0;
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
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(740, 130);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 29);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Démarrer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(740, 209);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(94, 29);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "Aide";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // Jeu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(921, 595);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.clbNote);
            this.Controls.Add(this.panelGrille);
            this.Name = "Jeu";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelGrille;
        private CheckedListBox clbNote;
        private Button btnStart;
        private Button btnHelp;
    }
}