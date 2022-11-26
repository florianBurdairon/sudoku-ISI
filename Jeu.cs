using System.Text.Json;
using System.Xml.Linq;

namespace sudoku
{
    public enum Difficulty
    {
        Facile,
        Moyen,
        Difficile,
        None
    }

    public partial class Jeu : Form
    {
        private SudokuCell[,] cells = new SudokuCell[9, 9];
        private int fullCells = 81;
        private int[] lastFocused = new int[2];
        public int time {get; private set;}
        public int nbHelp { get; private set; }
        private Leaderboard leaderboard;
        private Difficulty difficulty = Difficulty.None;
        private bool gameRunning = false;
        private bool existOldGame;

        public Jeu()
        {
            InitializeComponent();

            lbLeaderboard.Visible = false;
            listLeaderboard.Visible = false;

            try
            {
                int nbFullCells = getNbFullCellsOldGame();

                if(nbFullCells < 81)
                {
                    existOldGame = true;
                    // 
                    // btnContinue
                    // 
                    this.btnContinue = new Button();
                    this.panelGrille.Controls.Add(this.btnContinue);
                    this.btnContinue.Location = new System.Drawing.Point(277, 253);
                    this.btnContinue.Name = "btnContinue";
                    this.btnContinue.Size = new System.Drawing.Size(128, 29);
                    this.btnContinue.TabIndex = 3;
                    this.btnContinue.Text = "Reprendre la partie";
                    this.btnContinue.UseVisualStyleBackColor = true;
                    this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
                }
            }
            catch(Exception e)
            {
                existOldGame = false;
            }

            try
            {
                string jsonString = File.ReadAllText(@"..\..\..\Data\leaderboard.json");
                Leaderboard? tmp = JsonSerializer.Deserialize<Leaderboard>(jsonString);
                if (tmp != null)
                    leaderboard = tmp;
                else
                    leaderboard = new Leaderboard();
            }
            catch(Exception e)
            {
                leaderboard = new Leaderboard();
            }
            LoadLeaderboard();
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

                try
                {
                    cells[newCoords[0], newCoords[1]].Focus();
                }
                catch (Exception e) 
                {
                    cells[0, 0].Focus();
                }
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
                    // Use grid to fill cells
                    cells[i, j].SetOriginalValue(grid.GetPos(i, j));
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
                Victory();
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

                // Changer la couleur de la ligne, colonne et carré qui impacte la case
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        Color c = ((x / 3) + (y / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                        Color col = Color.FromArgb((int)(0.5f * c.R + 0.5f * 210f), (int)(0.5f * c.G + 0.5f * 210f), 255);
                        if (lastFocused[0] == x && lastFocused[1] == y)
                            cells[x, y].BackColor = ((x / 3) + (y / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                        else if (lastFocused[0] == x)
                            cells[x, y].BackColor = col;
                        else if (lastFocused[1] == y)
                            cells[x, y].BackColor = col;
                        else if (lastFocused[0] / 3 == x / 3 && lastFocused[1] / 3 == y / 3)
                            cells[x, y].BackColor = col;
                        else
                            cells[x, y].BackColor = ((x / 3) + (y / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
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

        private void Victory()
        {
            timer.Stop();
            ChoiceUsername cu = new ChoiceUsername(this);
            cu.Show();
        }

        public void SaveScore(string username = "Guest")
        {
            leaderboard.AddScore(username, time, nbHelp, difficulty);
            string fileName = @"..\..\..\Data\leaderboard.json";
            string jsonString = JsonSerializer.Serialize(leaderboard);
            File.WriteAllText(fileName, jsonString);
            LoadLeaderboard();
        }

        private void clbNotes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            cells[lastFocused[0], lastFocused[1]].SetNoteAt(e.Index, e.CurrentValue == CheckState.Unchecked);
            cells[lastFocused[0], lastFocused[1]].Focus();
        }

        private void DisplayLeaderboard()
        {

            lbLeaderboard.Text = "Tableau des scores (" + difficulty + ") :";
            lbLeaderboard.Visible = true;
            LoadLeaderboard();
            listLeaderboard.Visible = true;
        }

        private void InitializeStart()
        {
            panelGrille.Controls.Clear();
            clbNote.Visible = false;
            fullCells = 81;
            nbHelp = 0;

            difficulty = Difficulty.None;
            if (this.rbtnEasy.Checked)
                difficulty = Difficulty.Facile;
            else if (this.rbtnMedium.Checked)
                difficulty = Difficulty.Moyen;
            else if (this.rbtnHard.Checked)
                difficulty = Difficulty.Difficile;

            DisplayLeaderboard();

            cells = new SudokuCell[9, 9];
            createCells();

            Grille grid = new Grille();
            fillGrid(grid);
            removeCell(grid.RemoveCellsHard(difficulty));

            btnHelp.Text = "Aide (+1s)";

            AddToTime(0);
            timer.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeStart();

            btnStart.Visible = false;
            if(existOldGame)
                btnContinue.Visible = false;
            lbTime.Visible = true;
            btnRestart.Visible = true;
            btnHelp.Visible = true;

            gameRunning = true;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            InitializeStart();

            gameRunning = true;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (fullCells < 80) // impossible d'avoir de l'aide pour la dernière case
            {
                fullCells++;
                int x;
                int y;
                do
                {
                    x = Random.Shared.Next(9);
                    y = Random.Shared.Next(9);
                } while (cells[x, y].IsLocked || cells[x, y].Value != 0);

                cells[x, y].Help();

                // Ajouter du temps si utilisation de l'aide
                AddToTime((int)Math.Min(Math.Pow((2+(int)difficulty), nbHelp), 300 * (1f + (int)difficulty / 2f)));
                nbHelp++;
            }
            else
            {
                MessageBox.Show("Aide non disponible pour la dernière case.");
            }
            btnHelp.Text = "Aide (+" + (int)Math.Min(Math.Pow((2 + (int)difficulty), nbHelp), 300 * (1f + (int)difficulty / 2f)) + "s)";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            AddToTime(1);
        }

        private void AddToTime(int val)
        {
            if (val != 0)
                time += val;
            else
                time = 0;

            lbTime.Text = "Temps : ";
            if (time / 3600 > 0)
            {
                lbTime.Text += (time / 3600).ToString() + ":";
            }
            string m = ((time % 3600) / 60).ToString();
            if (m.Length == 1)
                m = "0" + m;
            string s = (time % 60).ToString();
            if (s.Length == 1)
                s = "0" + s;

            lbTime.Text += m + ":" + s; 
        }

        private string GetPossibleValues(SudokuCell[,] grid, int x, int y, string alreadyUsed = "")
        {
            int[,] ints = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    ints[i, j] = grid[i, j].Value;

            return Grille.GetPossibleValues(ints, x, y, alreadyUsed);
        }

        private void LoadLeaderboard()
        {
            listLeaderboard.Items.Clear();
            if (difficulty != Difficulty.None)
            {
                List<Score> lb = leaderboard.GetLeaderboardWith(difficulty);
                for (int i = 0; i < lb.Count; i++)
                {
                    listLeaderboard.Items.Add(lb[i].ToString());
                }
            }
        }

        private void serializeGame()
        {
            SaveGame save = new SaveGame(cells, fullCells, time, nbHelp, difficulty);
            string fileName = @"..\..\..\Data\save.json";
            string jsonString = JsonSerializer.Serialize(save);
            File.WriteAllText(fileName, jsonString);

        }

        private void deserializeGame()
        {
            string jsonString = File.ReadAllText(@"..\..\..\Data\save.json");
            SaveGame? save = JsonSerializer.Deserialize<SaveGame>(jsonString);
            this.time = save.time;
            this.nbHelp = save.nbHelp;
            this.fullCells = save.fullCells;
            this.cells = loadCells(save);
            this.lastFocused = new int[2] { 0, 0 };
            this.difficulty = save.difficulty;
        }

        private int getNbFullCellsOldGame() 
        {
            try
            {
                string jsonString = File.ReadAllText(@"..\..\..\Data\save.json");
                SaveGame? save = JsonSerializer.Deserialize<SaveGame>(jsonString);
                return save.fullCells;
            }
            catch(Exception e)
            {
                throw e;
                return -1;
            }
        }

        private SudokuCell[,] loadCells(SaveGame save)
        {
            SudokuCell[,] grid = new SudokuCell[9, 9];

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    grid[i, j] = new SudokuCell();
                    grid[i, j].Location = new Point(i * grid[i, j].Size.Width, j * grid[i, j].Size.Height);
                    grid[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    grid[i, j].X = save.grid[i * 9 + j].X;
                    grid[i, j].Y = save.grid[i * 9 + j].Y;
                    grid[i, j].SetOriginalValue(save.grid[i * 9 + j].OriginalValue);
                    grid[i, j].SetValue(save.grid[i * 9 + j].Value);
                    grid[i, j].SetNote(save.grid[i * 9 + j].Note);
                    grid[i, j].SetIsLocked(save.grid[i * 9 + j].IsLocked);

                    grid[i, j].KeyDown += cell_keyDown;
                    grid[i, j].Enter += cell_enterFocus;


                    panelGrille.Controls.Add(grid[i, j]);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    bool good = false;
                    string poss = GetPossibleValues(grid, i, j);
                    for (int k = 0; k < poss.Length; k++)
                        if (poss[k] - '0' == grid[i, j].Value)
                            good = true;
                    grid[i, j].SetIsGood(good);
                    
                }
            }

            return grid;
        }

        private void Jeu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (gameRunning)
                serializeGame();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            btnContinue.Visible = false;
            lbTime.Visible = true;
            btnRestart.Visible = true;
            btnHelp.Visible = true;

            gameRunning = true;

            panelGrille.Controls.Clear();
                        
            AddToTime(0);

            deserializeGame();

            DisplayLeaderboard();
            if (this.difficulty == Difficulty.Facile)
                rbtnEasy.Checked = true;
            else if (this.difficulty == Difficulty.Moyen)
                rbtnMedium.Checked = true;
            else if (this.difficulty == Difficulty.Difficile)
                rbtnHard.Checked = true;

            clbNote.Visible = false;
            btnHelp.Text = "Aide (+" + (int)Math.Min(Math.Pow((2 + (int)difficulty), nbHelp), 300 * (1f + (int)difficulty / 2f)) +"s)";

            timer.Start();
        }
    }
}