using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class SudokuCell : Button
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Clear()
        {
            this.Text = string.Empty;
            this.IsLocked = false;
            this.Value = 0;
        }

        public SudokuCell()
        {
            this.Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
            this.Size = new Size(40, 40);
            this.ForeColor = SystemColors.ControlDarkDark;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.Black;
        }
    }
}
