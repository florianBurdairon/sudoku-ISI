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

    /*
     * Fenêtre de gestion du jeu complet
     */
    public partial class Jeu : Form
    {
        private SudokuCell[,] cells = new SudokuCell[9, 9];
        private List<SudokuCell> wrongCells = new List<SudokuCell>();
        private int fullCells = 81;
        private int[] lastFocused = new int[2];
        public int time {get; private set;}
        public int nbHelp { get; private set; }
        private Leaderboard leaderboard;
        private Difficulty difficulty = Difficulty.None;
        private bool gameRunning = false;
        private bool existOldGame;

        private bool showBlueLines = true;

        private int[] leftNumbers = new int[9];

        public Jeu()
        {
            InitializeComponent();

            //Cache certains éléments du concepteur à la création de la fenêtre
            gbLeaderboard.Visible = false;
            groupLeftNumbers.Visible = false;
            cbBlueLines.Visible = false;

            //Ajout du bouton reprendre si une partie en cours à été trouvé
            try
            {
                int nbFullCells = getNbFullCellsOldGame();  //Lit le fichier save.json et récupère le nombre de cases remplies

                if(nbFullCells < 81)    //Si la grille n'est pas entièrement remplie
                {
                    existOldGame = true;

                    // Affiche le bouton btnContinue 
                    this.btnContinue = new Button();
                    this.panelGrille.Controls.Add(this.btnContinue);
                    this.btnContinue.Location = new System.Drawing.Point(264, 183);
                    this.btnContinue.Name = "btnContinue";
                    this.btnContinue.Size = new System.Drawing.Size(128, 55);
                    this.btnContinue.TabStop = false;
                    this.btnContinue.Text = "Reprendre la partie";
                    this.btnContinue.UseVisualStyleBackColor = true;
                    this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
                }else
                {
                    existOldGame = false;
                    this.btnStart.Location = new System.Drawing.Point(157, 183);    //Déplace le bouton btnStart pour le centrer s'il n'y a pas de partie en cours
                }
            }
            catch(Exception e)  //Exception possible si le fichier save.json n'existe pas
            {
                existOldGame = false;
                this.btnStart.Location = new System.Drawing.Point(157, 183);    //Déplace le bouton btnStart pour le centrer s'il n'y a pas de partie en cours
            }

            //Désérialise le fichier leaderboard.json pour récupérer les tableaux de scores
            try
            {
                string jsonString = File.ReadAllText(@"..\..\..\Data\leaderboard.json");
                Leaderboard? tmp = JsonSerializer.Deserialize<Leaderboard>(jsonString);
                if (tmp != null)
                    leaderboard = tmp;
                else
                    leaderboard = new Leaderboard();
            }
            catch(Exception e)  //Exception possible si le fichier leaderboard.json n'existe pas
            {
                leaderboard = new Leaderboard();
            }
            LoadLeaderboard();
        }

        /*
         * Surcharge le comportement par défaut des flèches de direction
         * Permet de naviguer dans la grille du sudoku
         */
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

            if (!gameRunning)
                return true;

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

        /*
         * Créer toutes les cases du sudoku
         */
        private void createCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Crée 81 SudokuCell avec un style et une position basé sur les indices i et j
                    cells[i, j] = new SudokuCell();
                    cells[i, j].Location = new Point(i * cells[i, j].Size.Width, j * cells[i, j].Size.Height);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.FromArgb(220, 220, 220);
                    cells[i, j].X = i;
                    cells[i, j].Y = j;

                    // Assigne les événements de chaque SudokuCell
                    cells[i, j].KeyDown += cell_keyDown;
                    cells[i, j].Enter += cell_enterFocus;

                    // Ajoute les SudokuCell dans le Panel
                    panelGrille.Controls.Add(cells[i, j]);
                }
            }
        }

        /*
         * Rempli la grille complète avec les chiffres de la grille finale
         */
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

        /*
         * Vide les cases du sudoku 
         */
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

        /*
         * Gère le clique sur une touche du clavier lorsqu'une cellule de la grille possède le focus
         */
        private void cell_keyDown(object sender, KeyEventArgs e)
        {
            if (!gameRunning)
                return;

            SudokuCell? cell = sender as SudokuCell;

            if (cell == null)
                return;

            if (cell.IsLocked)
                return;

            string poss = Grille.GetPossibleValues(getGridAsArray(), cell.X, cell.Y);
            bool isOldCorrect = false;
            bool isNewCorrect = false;

            foreach (char c in poss)
                if (c - '0' == cell.Value)
                    isOldCorrect = true;

            //Gère le cas où on souhaite supprimer la valeur précédemment saisi (touches : 0, SUPPR et retour en arrière)
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (cell.Value == 0) // Si la case est vide
                {
                    if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) // Et qu'on veut supprimer les annotations
                    {
                        cell.SetNoteFromString("");
                        for (int i = 0; i < 9; i++)
                        {
                            clbNote.SetItemChecked(i, cell.GetNote()[i]);
                        }
                    }
                }
                if (isOldCorrect)
                {
                    fullCells -= 1;
                }
                else if (cell.Value != 0)
                {
                    wrongCells.Remove(cell);
                }
                cell.Clear();
            }

            //Gère le cas où un chiffre a été saisi
            if ((e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9))
            {
                int value = e.KeyCode <= Keys.D9 ? (int)(e.KeyCode - Keys.D0) : (int)(e.KeyCode - Keys.NumPad0);

                foreach (char c in poss)
                    if (c - '0' == value)
                        isNewCorrect = true;

                if (isNewCorrect)
                {
                    if (!isOldCorrect)
                    {
                        fullCells += 1;
                        if (cell.Value != 0)
                            wrongCells.Remove(cell);
                    }
                    cell.ForeColor = Color.Black;
                    cell.SetValue(value);
                }
                else
                {
                    if (isOldCorrect)
                    {
                        fullCells -= 1;
                    }
                    else if (cell.Value != 0)
                    {

                            wrongCells.Remove(cell);
                        }
                        cell.ForeColor = Color.Red;
                        cell.SetValue(value);
                        wrongCells.Add(cell);
                        checkConflictCell(cell);
                    }
                    cell.SetValue(value);
                }

                CalculateLeftNumbers(); //Affiche le nombre restant de chacun des chiffres
                checkAllWrongCells();   //Vérifie si les cases précédemment fausses le sont toujours

            if (fullCells == 81)    //Condition de victoire
            {
                Victory();
            }
        }

        /*
         * Vérifie si les cases précédemment fausses le sont toujours
         */
        private void checkAllWrongCells()
        {
            List<SudokuCell> correctCells = new List<SudokuCell>();
            foreach (SudokuCell cell in wrongCells) //Vérifie toutes les cases fausses
            {
                string poss = Grille.GetPossibleValues(getGridAsArray(), cell.X, cell.Y);
                bool isPoss = false;
                foreach (char c in poss)
                    if (c - '0' == cell.Value)
                        isPoss = true;
                cell.SetIsGood(isPoss);
                if (isPoss)
                {
                    fullCells += 1;
                    correctCells.Add(cell);
                }
            }
            foreach(SudokuCell cell in correctCells)    //Retire les cases qui sont devenues correctes de la listes des cases fausses
            {
                wrongCells.Remove(cell);
            }
        }

        /*
         * Ajoute toutes les cases qui sont en conflits avec la cellule passée en paramètre dans la liste des cases fausses
         */
        private void checkConflictCell(SudokuCell cell)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!(cell.X == i && cell.Y == j))
                    {
                        if (!cells[i, j].IsLocked && cells[i, j].Value != 0) // Ne pas compter les cases locked + les non-remplis
                        {
                            bool alreadyIn = false; // Vérifier que la case n'est pas 2 fois dedans
                            foreach(SudokuCell c in wrongCells)
                                if (c.X == i && c.Y == j)
                                    alreadyIn = true;
                            if (!alreadyIn)
                            {
                                if (i == cell.X)    //Même ligne
                                {
                                    string poss = Grille.GetPossibleValues(getGridAsArray(), i, j);
                                    bool isPoss = false;
                                    foreach (char c in poss)
                                        if (c - '0' == cell.Value)
                                            isPoss = true;
                                    cells[i, j].SetIsGood(isPoss);
                                    if (!isPoss)
                                    {
                                        fullCells -= 1;
                                        wrongCells.Add(cells[i, j]);
                                    }
                                }
                                else if (j == cell.Y)   //Même colonne
                                {
                                    string poss = Grille.GetPossibleValues(getGridAsArray(), i, j);
                                    bool isPoss = false;
                                    foreach (char c in poss)
                                        if (c - '0' == cell.Value)
                                            isPoss = true;
                                    cells[i, j].SetIsGood(isPoss);
                                    if (!isPoss)
                                    {
                                        fullCells -= 1;
                                        wrongCells.Add(cells[i, j]);
                                    }
                                }
                                else if (i / 3 == cell.X / 3 && j / 3 == cell.Y / 3)    //Même carré
                                {
                                    string poss = Grille.GetPossibleValues(getGridAsArray(), i, j);
                                    bool isPoss = false;
                                    foreach (char c in poss)
                                        if (c - '0' == cell.Value)
                                            isPoss = true;
                                    cells[i, j].SetIsGood(isPoss);
                                    if (!isPoss)
                                    {
                                        fullCells -= 1;
                                        wrongCells.Add(cells[i, j]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /*
         * Gère le fait qu'une cellule reçoive le focus
         */
        private void cell_enterFocus(object? sender, EventArgs e)
        {
            var cell = sender as SudokuCell;
            if (cell != null)
            {
                lastFocused = new int[] { cell.X, cell.Y };

                if (clbNote != null)
                {
                    if (cell.IsLocked)
                        gbNotes.Visible = false;
                    else
                        gbNotes.Visible = true;
                    for (int i = 0; i < 9; i++)
                    {
                        clbNote.SetItemChecked(i, cell.GetNote()[i]);
                    }
                }

                DrawBlueLines();    //Change la couleur des cases affectées par la case possèdant le focus
            }
        }

        /*
         * Permeet de créer un tableau d'entier à 2 dimensions contenant les valeurs de toutes les cases de la grille
         */
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

        /*
         * Gère la fin de partie
         */
        private void Victory()
        {
            timer.Stop();   //Arrête le chronomètre
            ChoiceUsername cu = new ChoiceUsername(this);   //Crée et affiche une nouvelle fenêtre permettant d'enregistrer le score
            cu.Show();                                      //
        }

        /*
         * Permet d'enregistrer le score et de sérialiser la listes des scores dans le fichier leaderboard.json
         */
        public void SaveScore(string username = "Guest")
        {
            leaderboard.AddScore(username, time, nbHelp, difficulty);
            string fileName = @"..\..\..\Data\leaderboard.json";
            string jsonString = JsonSerializer.Serialize(leaderboard);
            File.WriteAllText(fileName, jsonString);
            LoadLeaderboard();
        }

        /*
         * Ajoute des annotations sur la cellules possèdant le focus
         */
        private void clbNotes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            cells[lastFocused[0], lastFocused[1]].SetNoteAt(e.Index, e.CurrentValue == CheckState.Unchecked);
            cells[lastFocused[0], lastFocused[1]].Focus();
        }

        /*
         * Affiche le tableau des scores en fonction de la difficulté
         */
        private void DisplayLeaderboard()
        {

            gbLeaderboard.Text = "Tableau des scores (" + difficulty + ") :";
            gbLeaderboard.Visible = true;
            LoadLeaderboard();
            listLeaderboard.Visible = true;
        }

        /*
         * Permet de démarrer la partie
         */
        private void InitializeStart()
        {
            panelGrille.Controls.Clear();
            gbNotes.Visible = false;
            fullCells = 81;
            nbHelp = 0;
            wrongCells.Clear();
            line1.Visible = true;
            line2.Visible = true;
            line3.Visible = true;
            line4.Visible = true;

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

            CalculateLeftNumbers();

            btnHelp.Text = "Aide (+1s)";

            AddToTime(0);
            timer.Start();
        }

        /*
         * Gère le clique sur le bouton btnStart
         */
        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeStart();

            btnStart.Visible = false;
            if(existOldGame)
                btnContinue.Visible = false;
            lbTime.Visible = true;
            btnRestart.Visible = true;
            btnHelp.Visible = true;
            groupLeftNumbers.Visible = true;
            cbBlueLines.Visible = true;

            gameRunning = true;
        }

        /*
         * Gère le clique sur le bouton btnRestart
         */
        private void btnRestart_Click(object sender, EventArgs e)
        {
            InitializeStart();

            gameRunning = true;
        }

        /*
         * Gère le clique sur le bouton btnHelp
         */
        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (fullCells < 80) // impossible d'avoir de l'aide pour la dernière case
            {
                List<SudokuCell> emptyCells = new List<SudokuCell>();

                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                        if (!cells[x, y].IsLocked && cells[x, y].Value == 0)
                            emptyCells.Add(cells[x, y]);

                if (emptyCells.Count > 0)
                {
                    int randInd = Random.Shared.Next(emptyCells.Count); //Sélectionne aléatoirement une case vide

                    cells[emptyCells[randInd].X, emptyCells[randInd].Y].Help(); //Rempli la case précédemment sélectionnée avec la bonne valeur
                    fullCells++;

                    // Ajouter du temps si utilisation de l'aide
                    AddToTime((int)Math.Min(Math.Pow((2 + (int)difficulty), nbHelp), 300 * (1f + (int)difficulty / 2f)));
                    nbHelp++;
                }
                else
                {
                    MessageBox.Show("Il n'existe aucune case où l'aide peut être utilisée");
                }
            }
            else
            {
                MessageBox.Show("Aide non disponible pour la dernière case.");
            }
            btnHelp.Text = "Aide (+" + (int)Math.Min(Math.Pow((2 + (int)difficulty), nbHelp), 300 * (1f + (int)difficulty / 2f)) + "s)";
        }

        /*
         * Incrément un compteur à la fin de chaque intervalle de 1 seconde du Timer
         */
        private void timer_Tick(object sender, EventArgs e)
        {
            AddToTime(1);
        }

        /*
         * Ajoute du temps au compteur et met à jour l'affichage
         */
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

        /*
         * Récupère une chaîne de caractères contenant tous les chiffres possibles pour la case passée en paramètre
         */
        private string GetPossibleValues(SudokuCell[,] grid, int x, int y, string alreadyUsed = "")
        {
            int[,] ints = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    ints[i, j] = grid[i, j].Value;

            return Grille.GetPossibleValues(ints, x, y, alreadyUsed);
        }

        /*
         * Affiche le nombre de chiffre restant pour chacun des chiffres
         */
        private void CalculateLeftNumbers()
        {
            leftNumbers = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9};
            if (cells != null)
            {
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        if (cells[x, y] != null)
                        {
                            if (cells[x, y].Value != 0)
                                leftNumbers[cells[x, y].Value - 1]--;
                        }
                    }
                lbLeftNumbers.Items.Clear();
                for (int i = 0; i < leftNumbers.Length; i++)
                {
                    if (leftNumbers[i] > 0)
                        lbLeftNumbers.Items.Add(leftNumbers[i] + " fois \"" + (i + 1) + "\"");
                    else if (leftNumbers[i] == 0)
                        lbLeftNumbers.Items.Add("Aucun \"" + (i + 1) + "\"");
                    else
                        lbLeftNumbers.Items.Add("Trop de \"" + (i + 1) + "\"");
                }
            }
        }

        /*
         * Permet d'afficher les scores de la difficulté sélectionnée
         */
        private void LoadLeaderboard()
        {
            listLeaderboard.Items.Clear();  //Vide la ListBox
            if (difficulty != Difficulty.None)
            {
                List<Score> lb = leaderboard.GetLeaderboardWith(difficulty);
                for (int i = 0; i < lb.Count; i++)  //Ajoute les scores correspondant à la bonne difficulté
                {
                    listLeaderboard.Items.Add(lb[i].ToString());
                }
            }
        }

        /*
         * Permet de sauvegarder la partie en cours en sérialisant les données dans le fichier save.json
         */
        private void serializeGame()
        {
            SaveGame save = new SaveGame(cells, fullCells, time, nbHelp, difficulty);
            string fileName = @"..\..\..\Data\save.json";
            string jsonString = JsonSerializer.Serialize(save);
            File.WriteAllText(fileName, jsonString);

        }

        /*
         * Permet de charger une partie existante en désérialisant le fichier save.json
         */
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

        /*
         * Permet de récupérer le nombre de case remplie dans la grille d'une ancienne partie stockée dans le fichier save.jsom
         */
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
            }
        }

        /*
         * Permet de générer la grille à partir de la sauvegarde d'une ancienne partie
         */
        private SudokuCell[,] loadCells(SaveGame save)
        {
            SudokuCell[,] grid = new SudokuCell[9, 9];

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    grid[i, j] = new SudokuCell();
                    grid[i, j].Location = new Point(i * grid[i, j].Size.Width, j * grid[i, j].Size.Height);
                    grid[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.FromArgb(220, 220, 220);
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

        /*
         * Permet de sérialiser la partie en cours à la fermeture de la fenêtre
         */
        private void Jeu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (gameRunning)
                serializeGame();
        }

        /*
         * Gère le clique sur le bouton btnContinue
         */
        private void btnContinue_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            btnContinue.Visible = false;
            lbTime.Visible = true;
            btnRestart.Visible = true;
            btnHelp.Visible = true;
            groupLeftNumbers.Visible = true;
            cbBlueLines.Visible = true;
            line1.Visible = true;
            line2.Visible = true;
            line3.Visible = true;
            line4.Visible = true;

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

            CalculateLeftNumbers();

            gbNotes.Visible = false;
            btnHelp.Text = "Aide (+" + (int)Math.Min(Math.Pow((2 + (int)difficulty), nbHelp), 300 * (1f + (int)difficulty / 2f)) +"s)";

            timer.Start();
        }

        /*
         * Permet de changer la couleur des cases affectées par la case possédant le focus
         */
        private void DrawBlueLines()
        {
            if (showBlueLines)
            {
                // Changer la couleur de la ligne, colonne et carré qui impacte la case
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        Color c = ((x / 3) + (y / 3)) % 2 == 0 ? SystemColors.Control : Color.FromArgb(220, 220, 220);
                        //Color col = Color.FromArgb((int)(0.5f * c.R + 0.5f * 210f), (int)(0.5f * c.G + 0.5f * 210f), 255);
                        Color col = Color.FromArgb(c.R, c.G, 255);
                        if (lastFocused[0] == x && lastFocused[1] == y)
                            cells[x, y].BackColor = c;
                        else if (lastFocused[0] == x)
                            cells[x, y].BackColor = col;
                        else if (lastFocused[1] == y)
                            cells[x, y].BackColor = col;
                        else if (lastFocused[0] / 3 == x / 3 && lastFocused[1] / 3 == y / 3)
                            cells[x, y].BackColor = col;
                        else
                            cells[x, y].BackColor = c;
                    }
            }
            else
            {
                // Remettre à zéro la couleur de la ligne, colonne et carré qui impacte la case
                for (int x = 0; x < 9; x++)
                    for (int y = 0; y < 9; y++)
                    {
                        cells[x, y].BackColor = ((x / 3) + (y / 3)) % 2 == 0 ? SystemColors.Control : Color.FromArgb(220, 220, 220);
                    }
            }
        }

        /*
         * Gère le clique sur la checkbox cbBlueLines
         */
        private void cbBlueLines_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox? cb = sender as CheckBox;
            if (cb != null)
            {
                showBlueLines = cb.Checked;
                DrawBlueLines();
                cells[lastFocused[0], lastFocused[1]].Focus();
            }
        }
    }
}