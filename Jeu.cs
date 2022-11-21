namespace sudoku
{
    public partial class Jeu : Form
    {
        SudokuCell[,] cells = new SudokuCell[9, 9];
        int fullCells = 81;
        int[] lastFocused = new int[2];
        //System.Windows.Forms.CheckedListBox clbNotes;

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
                    newCoords[0] = 1;
                    break;
                case Keys.Left:
                    newCoords[0] = -1;
                    break;
                case Keys.Up:
                    newCoords[1] = -1;
                    break;
                case Keys.Down:
                    newCoords[1] = 1;
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);

            }

            if (newCoords != new int[] { 0, 0 })
            {

                foreach (SudokuCell cell in cells)
                    if (cell.Focused)
                    {
                        newCoords[0] = (newCoords[0] + cell.X) % 9;
                        newCoords[1] = (newCoords[1] + cell.Y) % 9;
                        if (newCoords[0] == -1) newCoords[0] = 8;
                        if (newCoords[1] == -1) newCoords[1] = 8;
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
                    cells[i, j].Location = new Point(i * cells[i, j].Size.Width, j * cells[i, j].Size.Height);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    cells[i, j].X = i;
                    cells[i, j].Y = j;

                    // Assign key press event for each cells
                    cells[i, j].KeyDown += cell_keyDown;
                    cells[i, j].Enter += cell_enterFocus;


                    panelGrille.Controls.Add(cells[i, j]);
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
                    cells[i, j].SetValue(grid.GetPos(i, j));
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
                        fullCells -= 1;
                        cells[i, j].Clear();
                    }
                    else
                    {
                        cells[i, j].SetIsLocked(true);
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

            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (cell.Value != 0)
                    fullCells -= 1;
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
                    if (cell.ForeColor == Color.Red || cell.Value == 0)
                        fullCells += 1;
                    cell.SetIsGood(true);
                }
                else
                {
                    if (cell.ForeColor == Color.Black || cell.Value == 0)
                        fullCells -= 1;
                    cell.SetIsGood(false);
                }
                cell.SetValue(value);
            }

            if (fullCells == 81)
            {
                MessageBox.Show("Victory!");
            }
        }

        private void cell_enterFocus(object? sender, EventArgs e)
        {
            var cell = sender as SudokuCell;
            if (cell != null)
            {
                lastFocused = new int[] { cell.X, cell.Y };

                if (clbNote != null)
                {
                    if (cell.IsLocked)
                        clbNote.Visible = false;
                    else
                        clbNote.Visible = true;
                    for (int i = 0; i < 9; i++)
                    {
                        clbNote.SetItemChecked(i, cell.GetNote()[i]);
                    }
                }


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

        private void clbNotes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            cells[lastFocused[0], lastFocused[1]].SetNoteAt(e.Index, e.CurrentValue == CheckState.Unchecked);
            cells[lastFocused[0], lastFocused[1]].Focus();
        }
    }
}