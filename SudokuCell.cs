﻿namespace sudoku
{
    class SudokuCell : Button
    {
        public int Value { get; private set; }
        public bool IsLocked { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        private bool[] Note = new bool[9] { false, false, false, false, false, false, false, false, false };

        // Reset la case
        public void Clear()
        {
            this.SetValue(0);
            this.SetIsLocked(false);
            this.SetTextAsNote(true);
        }

        // Constructeur par défaut, sans paramètre
        public SudokuCell()
        {
            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.Size = new Size(50, 50);
            this.ForeColor = SystemColors.ControlDarkDark;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Black;

        }

        // Constructeur avec paramètres
        public SudokuCell(int value = 0, bool islocked = false, bool isgood = true)
        {
            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.Size = new Size(50, 50);
            this.ForeColor = SystemColors.ControlDarkDark;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Black;


            this.SetValue(value);
            this.SetIsLocked(islocked);
            this.SetTextAsNote(true);
        }

        // Change la valeur dans la case selon *value*
        public void SetValue(int value = 0)
        {
            if (!this.IsLocked)
            {
                this.Value = value;
                if (value == 0)
                {
                    if (this.IsNoteEmpty())
                        this.Text = string.Empty;
                    else
                    {
                        SetTextAsNote(true);
                    }
                }
                else
                {
                    SetTextAsNote(false);
                }
            }
        }

        // Défini si la valeur de la case peut être changée par l'utilisateur
        public void SetIsLocked(bool islocked = true)
        {
            this.IsLocked = islocked;
        }

        // Change la couleur si la valeur est possible
        public void SetIsGood(bool isgood = true)
        {
            if (isgood)
                this.ForeColor = Color.Black;
            else
                this.ForeColor = Color.Red;
        }

        // Défini si l'annotation *index* est vraie ou fausse : *val*
        public void SetNoteAt(int index, bool val)
        {
            this.Note[index] = val;
            if (this.Value == 0)
            {
                SetTextAsNote(true);
            }
        }

        // Retourne true si il n'y a aucune annotation sur la case
        private bool IsNoteEmpty()
        {
            foreach (bool b in Note)
            {
                if (b) return false;
            }
            return true;
        }

        // Permet de définir les annotations grâce à un string
        private void SetNoteFromString(string note)
        {
            foreach(char c in note)
            {
                this.Note[c - '1'] = true;
            }
        }

        // Retourne l'ensemble des annotations sous forme de string
        private string GetStringFromNote()
        {
            string ret = "";
            for(int i = 0; i < this.Note.Length; i++)
            {
                if (this.Note[i])
                    ret += (i+1);
            }

            return ret;
        }

        // Retourne l'ensemble des annotations sous forme de tableau de booléans
        public bool[] GetNote()
        {
            return this.Note;
        }

        // Change les paramètres si le texte de la case est une annotation (Note!=null) ou une valeur
        private void SetTextAsNote(bool isNote)
        {
            if (isNote)
            {
                int fontsize = Math.Max(14 - GetStringFromNote().Length, 6);
                this.Font = new Font(this.Font.Name, fontsize, FontStyle.Italic);
                this.Text = GetStringFromNote();
                this.ForeColor = Color.Blue;
            }
            else
            {
                this.Font = new Font(this.Font.Name, 20, FontStyle.Bold);
                this.Text = this.Value.ToString();
            }
        }
    }
}
