using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class SudokuCell : Button
    {
        public int Value { get; private set; }
        public bool IsLocked { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        private bool[] Note = new bool[9] { false, false, false, false, false, false, false, false, false };

        public void Clear()
        {
            this.SetValue(0);
            this.SetIsLocked(false);
            this.SetIsGood(true);
        }

        public SudokuCell()
        {
            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.Size = new Size(50, 50);
            this.ForeColor = SystemColors.ControlDarkDark;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Black;

        }

        public SudokuCell(int value = 0, bool islocked = false, bool isgood = true)
        {
            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.Size = new Size(50, 50);
            this.ForeColor = SystemColors.ControlDarkDark;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Black;


            this.SetValue(value);
            this.SetIsLocked(islocked);
            this.SetIsGood(isgood);
        }

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
                        this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 6, FontStyle.Italic);
                        this.Text = GetStringFromNote();
                    }
                }
                else
                {
                    this.Font = new Font(this.Font.Name, 20, FontStyle.Bold);
                    this.Text = value.ToString();
                }
            }
        }

        public void SetIsLocked(bool islocked = true)
        {
            this.IsLocked = islocked;
        }

        public void SetIsGood(bool isgood = true)
        {
            if (isgood)
                this.ForeColor = Color.Black;
            else
                this.ForeColor = Color.Red;
        }

        public void SetNote(string note)
        {
            SetNoteFromString(note);
            if (this.Value == 0)
            {
                this.Font = new Font(this.Font.Name, 6, FontStyle.Italic);
                this.Text = GetStringFromNote();
            }
        }

        public void SetNote(int index, bool val)
        {
            this.Note[index] = val;
            if (this.Value == 0)
            {
                this.Font = new Font(this.Font.Name, 6, FontStyle.Italic);
                this.Text = GetStringFromNote();
            }
        }

        private bool IsNoteEmpty()
        {
            foreach (bool b in Note)
            {
                if (b) return false;
            }
            return true;
        }

        private void SetNoteFromString(string note)
        {
            foreach(char c in note)
            {
                this.Note[c - '1'] = true;
            }
        }

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

        public bool[] GetNote()
        {
            return this.Note;
        }
    }
}
