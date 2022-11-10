using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{

    

    internal class Grille
    {
        internal class Case : IComparable<Case>
        {
            static public int xLast = 1;
            static public int yLast = 1;
            public int x;
            public int y;
            public bool[] possibilites;
            public int nbPoss;

            public Case()
            {
                x = xLast;
                y = yLast;
                if (xLast == 9)
                {
                    xLast = 1;
                    yLast += 1;
                }

                possibilites = new bool[9] { true, true, true, true, true, true, true, true, true };
            }

            static public void resetLasts()
            {
                xLast = 0;
                yLast = 0;
            }

            public int CompareTo(Case? obj)
            {
                if (obj == null)
                    return 1;

                return this.nbPoss.CompareTo(obj.nbPoss);
            }
        }

        // Grille complète
        private int[,] grille;

        public Grille()
        {
            grille = new int[9, 9];
            GenerateGrid();
        }

        // Construire Grille en utilisant *Case*[9][9]
        public void GenerateGrid()
        {
            Case[] cases = new Case[81];
            Case.resetLasts();
            Array.Sort(cases);
            cases[0] = new Case();
        }

        // Retourne la valeur à la position (x, y)
        public int GetPos(int x, int y)
        {
            return grille[x, y];
        }

        public override string ToString()
        {
            string ret = "Grille = \n ";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ret += grille[i, j] + ", ";
                }
                ret += "\n ";
            }

            return ret;
        }
    }
}