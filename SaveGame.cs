using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sudoku
{
    /*
     * Classe intermédiaire permettant la sérialisation des informations de la classe Jeu
     */
    [Serializable]
    internal class SaveGame
    {
        /*
         * Classe interne de SaveGame permettant de sérialiser les informations de la classe SudokuCell
         */
        [Serializable]
        internal class Cell
        {
            public int Value { get; set; } = 0;
            public int OriginalValue { get; set; } = 0;
            public bool IsLocked { get; set; } = false;
            public int X { get; set; } = 0;
            public int Y { get; set; } = 0;
            public bool[] Note { get; set; } = new bool[9];

            public Cell() { }

            public Cell(int Value, int OriginalValue, bool IsLocked, int X, int Y, bool[] Note)
            {
                this.Value = Value;
                this.OriginalValue = OriginalValue;
                this.IsLocked = IsLocked;
                this.X = X;
                this.Y = Y;
                this.Note = Note;
            }
        }

        public int fullCells { get; set; } = 81;
        public int time { get; set; }
        public int nbHelp { get; set; }
        public Cell[] grid { get; set; } = new Cell[81];
        public Difficulty difficulty { get; set;}

        /*
         * Constructeur utilisé lors de la désérialisation du fichier json
         */
        [JsonConstructor]
        public SaveGame(int fullCells, int time, int nbHelp, Cell[] grid, Difficulty difficulty)
        {

            this.fullCells = fullCells;
            this.time = time;
            this.nbHelp = nbHelp;
            this.grid = grid;
            this.difficulty = difficulty;
        }

        /*
         * Constructeur utilisé pour créer SaveGame avant la sérialisation
         */
        public SaveGame(SudokuCell[,] cells, int fullCells, int time, int nbHelp, Difficulty difficulty)
        {
            
            this.fullCells = fullCells;
            this.time = time;
            this.nbHelp = nbHelp;
            for(int i = 0; i < grid.Length; i++)
            {
                grid[i] = new Cell(cells[i / 9, i % 9].Value, cells[i / 9, i % 9].GetOriginalValue(), cells[i / 9, i % 9].IsLocked, cells[i / 9, i % 9].X, cells[i / 9, i % 9].Y, cells[i / 9, i % 9].GetNote());
            }
            this.difficulty = difficulty;
        }
    }
}
