using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{

    

    internal class Grille
    {
        internal class Case : IComparable<Case>
        {
            private int x;
            private int y;
            private bool[] possibilites;
            private int nbPoss;

            public Case(int _x, int _y)
            {
                x = _x;
                y = _y;
                possibilites = new bool[9] { true, true, true, true, true, true, true, true, true };
                nbPoss = possibilites.Length;
            }

            public int getX()
            {
                return x;
            }

            public int getY()
            {
                return y;
            }

            // Retourne une valeur aléatoire parmi celles possibles pour la case
            public int getRandomPoss()
            {
                // Générer un nombre aléatoire entre 0 et le nombre de possibilités
                int randNb = Random.Shared.Next(nbPoss) ;
                // Récupérer la valeur associée à la randNb-ième valeur possible
                for(int i = 0; i < possibilites.Length; i++)
                {
                    if (possibilites[i] == true)
                    {
                        if (randNb == 0)
                            return i;
                        randNb--;
                    }
                }
                // Pas de valeur possible ?!
                return -1;
            }

            // Supprimer la possibilité si elle a deja été utilisée sur l'une des cases déterminantes
            public void check(int ox, int oy, int nb)
            {
                // Si plus aucune possibilité
                if (nbPoss == 0)
                {
                    MessageBox.Show("La case " + ox + ", " + oy + " n'a plus aucune possibilité ! (check)", "Erreur Génération", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Même ligne OU même colonne:
                if (x == ox || y == oy)
                {
                    if (possibilites[nb])
                    {
                        possibilites[nb] = false;
                        nbPoss--;
                    }
                }
                // Même carré
                else if (x/3 == ox/3 && y/3 == oy/3)
                {
                    if (possibilites[nb])
                    {
                        possibilites[nb] = false;
                        nbPoss--;
                    }
                }

            }

            // Comparer deux cases, comparaison faites sur leur nombre de possibilités
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
            List<string> debug = new List<string>();

            // ## Etape 1 ##
            // Tableau de 81 cases (temporaire)
            //Case[] cases = new Case[81];
            List<Case> cases = new List<Case>();

            // Remplissage du tableau
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    //cases[y * 9 + x] = new Case(x, y);
                    cases.Add(new Case(x, y));

            while (cases.Count > 0)
            {
                // ## Etape 2 ##
                // Trier le tableau par ordre croissant
                //Array.Sort(cases);
                cases.Sort();

                // ## Etape 3 ##
                int index = 0;
                Case ca = cases[index];

                // ## Etape 4 ##
                int poss = ca.getRandomPoss();
                if (poss == -1)
                    MessageBox.Show("La case " + index + " ne renvoie aucune possibilité ! (poss)", "Erreur Génération", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // ## Etape 5 ##
                grille[ca.getX(), ca.getY()] = poss+1; // poss+1 pck poss est un index entre 0 et 8

                // ## Etape 7 ##
                cases.RemoveAt(index);

                // ## Etape 6 ##
                foreach (Case c in cases)
                {
                    c.check(ca.getX(), ca.getY(), poss);
                }

                debug.Add(debug.Count() + " : " + ca.getX() + ", " + ca.getY() + " -> " + (poss+1));
            }

            MessageBox.Show("STOP", "Debug ;)", MessageBoxButtons.OK, MessageBoxIcon.Information);
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