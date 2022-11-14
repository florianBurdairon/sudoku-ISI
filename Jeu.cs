namespace sudoku
{
    public partial class Jeu : Form
    {
        SudokuCell[,] cells = new SudokuCell[9, 9];

        public Jeu()
        {
            InitializeComponent();
            createCells();

            Grille grid= new Grille();
            fillGrid(grid);
            removeCell(grid.removeCells());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int[] newCoords = new int[] {0, 0};
            switch (keyData)
            {
                case Keys.Right:
                    newCoords[0] = (newCoords[0] + 1) % 9;
                    break;
                case Keys.Left:
                    newCoords[0] = (newCoords[0] - 1) % 9;
                    break;
                case Keys.Up:
                    newCoords[1] = (newCoords[1] - 1) % 9;
                    break;
                case Keys.Down:
                    newCoords[1] = (newCoords[1] + 1) % 9;
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);

            }

            if (newCoords != new int[] { 0, 0 })
            {
                foreach (SudokuCell cell in cells)
                    if (cell.Focused)
                    {
                        newCoords[0] += cell.X;
                        newCoords[1] += cell.Y;
                    }

                cells[newCoords[0], newCoords[1]].Focus();
                return true;
            }

            return true;
        }

        private void createCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Create 81 cells for with styles and locations based on the index
                    cells[i, j] = new SudokuCell();
                    cells[i, j].Location = new Point(i * 40, j * 40);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    cells[i, j].X = i;
                    cells[i, j].Y = j;

                    // Assign key press event for each cells
                    cells[i, j].KeyDown += cell_keyDown;

                    panel1.Controls.Add(cells[i, j]);
                }
            }
        }

        public void fillGrid(Grille grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Use grille to fill cells
                    cells[i, j].Text = "" + grid.GetPos(i, j);
                    cells[i, j].Value = grid.GetPos(i, j);
                }
            }
        }

        public void removeCell(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Use grille to fill cells
                    if (grid[i, j] == 0)
                    {
                        cells[i, j].Clear();
                        cells[i, j].Font = new Font(cells[i, j].Font.Name, cells[i, j].Font.Size, FontStyle.Bold);
                    }
                    else
                    {
                        cells[i, j].IsLocked = true;
                    }
                }
            }
        }

        private void cell_keyDown(object sender, KeyEventArgs e)
        {
            var cell = sender as SudokuCell;

            // Do nothing if the cell is locked
            if (cell.IsLocked)
                return;

            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                cell.Clear();
            }    

            // Add the pressed key value in the cell only if it is a number
            if ((e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9))
            {
                int value = e.KeyCode <= Keys.D9 ? (int)(e.KeyCode - Keys.D0) : (int)(e.KeyCode - Keys.NumPad0);

                // Is the value the only one possible :
                string poss = Grille.GetPossibleValues(getGridAsArray(), cell.X, cell.Y);
                bool isPoss = false;
                foreach (char c in poss)
                    if (c - '0' == value)
                        isPoss = true;
                if (isPoss)
                {
                    cell.ForeColor = Color.Black;
                }
                else
                {
                    cell.ForeColor = Color.Red;
                }
                cell.Text = value.ToString();
                cell.Value = value;
            }
        }

        private int[,] getGridAsArray()
        {
            int[,] ret = new int[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ret[i, j] = cells[i, j].Value;
                }
            }

            return ret;
        }
    }
}