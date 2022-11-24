namespace sudoku
{
    partial class ChoiceUsername
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNotSave = new System.Windows.Forms.Button();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.labelVictory = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(60, 177);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 29);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Enregistrer";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNotSave
            // 
            this.btnNotSave.Location = new System.Drawing.Point(283, 177);
            this.btnNotSave.Name = "btnNotSave";
            this.btnNotSave.Size = new System.Drawing.Size(156, 29);
            this.btnNotSave.TabIndex = 1;
            this.btnNotSave.Text = "Ne pas enregistrer";
            this.btnNotSave.UseVisualStyleBackColor = true;
            this.btnNotSave.Click += new System.EventHandler(this.btnNotSave_Click);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(259, 134);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(125, 27);
            this.tbUsername.TabIndex = 2;
            // 
            // labelVictory
            // 
            this.labelVictory.AutoSize = true;
            this.labelVictory.Location = new System.Drawing.Point(186, 30);
            this.labelVictory.Name = "labelVictory";
            this.labelVictory.Size = new System.Drawing.Size(128, 20);
            this.labelVictory.TabIndex = 3;
            this.labelVictory.Text = "Vous avez gagné !";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(173, 74);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(144, 20);
            this.lbTime.TabIndex = 4;
            this.lbTime.Text = "Votre temps est de : ";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(149, 137);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(85, 20);
            this.labelUsername.TabIndex = 5;
            this.labelUsername.Text = "Votre nom :";
            // 
            // ChoiceUsername
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 258);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.labelVictory);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.btnNotSave);
            this.Controls.Add(this.btnSave);
            this.Name = "ChoiceUsername";
            this.Text = "Victoire !";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSave;
        private Button btnNotSave;
        private TextBox tbUsername;
        private Label labelVictory;
        private Label lbTime;
        private Label labelUsername;
    }
}