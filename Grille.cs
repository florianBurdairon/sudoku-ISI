using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public partial class Grille
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
        public int[,] grille { get; }

        public Grille()
        {
            grille = new int[9, 9];
            GenerateGrid();
            //grille = removeCells();
        }

        // Construire Grille en utilisant *Case*[9][9]
        public void GenerateGridOld()
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

        public string GetPossibleValues(int[,] grid, int nb, string alreadyUsed = "")
        {
            return GetPossibleValues(grid, nb % 9, nb / 9, alreadyUsed);
        }

        static public string GetPossibleValues(int[,] grid, int ox, int oy, string alreadyUsed = "")
        {
            string val = "123456789";

            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                {
                    if (ox == x || oy == y) // Même ligne ou même colonne
                    {
                        int index = val.IndexOf(grid[x, y].ToString());
                        if (index >= 0)
                            val = val.Remove(index, 1);
                    }
                    else if (x / 3 == ox / 3 && y / 3 == oy / 3) // Même carré
                    {
                        int index = val.IndexOf(grid[x, y].ToString());
                        if (index >= 0)
                            val = val.Remove(index, 1);
                    }
                }

            // Départ à i = 2 pck alreadyUsed possède X et Y dedans
            for(int i = 2; i < alreadyUsed.Length; i++)
            {
                int index = val.IndexOf(alreadyUsed[i]);
                if (index >= 0)
                    val = val.Remove(index, 1);
            }

            return val;
        }

        public void GenerateGrid()
        {
            // On stocke chaque étape qu'on va faire sour cette forme :
            // "X Y R1 R2 R3 . . . R9"
            // X et Y sont la position de la case utilisée
            // R1 R2 R3 ... R9 sont les valeurs déjà testées sur cette case, il peut y en avoir entre 0 et 9
            List<string> pile = new List<string>();

            // Nombre de cases remplies
            int nbCells = 0;

            // Première case :
            string cell = GetPossibleValues(grille, nbCells);
            int index = Random.Shared.Next(cell.Length);
            grille[nbCells % 9, nbCells / 9] = cell[index] - '0';
            pile.Add("" + nbCells % 9 + nbCells / 9 + grille[nbCells % 9, nbCells / 9]);
            nbCells++;

            // Tant qu'il reste des cases à remplir
            while (nbCells < 81)
            {
                // Si on doit remonter jusqu'à la première case (pas sur que ce soit fonctionnel)
                if (nbCells == 0)
                {
                    cell = GetPossibleValues(grille, nbCells, pile.Last<string>());
                    index = Random.Shared.Next(cell.Length);
                    grille[nbCells % 9, nbCells / 9] = cell[index] - '0';
                    pile.Add("" + nbCells % 9 + nbCells / 9 + grille[nbCells % 9, nbCells / 9]);
                    nbCells++;
                }

                // Si la dernière action a été faite sur la case actuelle, on utilise la pile
                if (pile.Last<string>()[0] - '0' == nbCells % 9 && pile.Last<string>()[1] - '0' == nbCells / 9)
                {
                    cell = GetPossibleValues(grille, nbCells, pile.Last<string>());
                    // Pas de possibilité pour la case actuelle : on remonte d'une case en supprimant le dessus de la pile
                    if (cell.Length == 0)
                    {
                        nbCells--;
                        grille[nbCells % 9, nbCells / 9] = '0';
                        pile.RemoveAt(pile.Count - 1);
                    }
                }
                // Sinon, on ne l'utilise pas
                else
                {
                    cell = GetPossibleValues(grille, nbCells);
                    // Pas de possibilité pour la case actuelle : on remonte d'une case
                    if (cell.Length == 0)
                    {
                        nbCells--;
                        grille[nbCells % 9, nbCells / 9] = '0';
                    }
                }

                // On peut donner une valeur à la case
                if (cell.Length != 0)
                {
                    // On récupère une valeur random parmi celles possibles
                    index = Random.Shared.Next(cell.Length);

                    // On donne cette valeur à la case dans la grille
                    grille[nbCells % 9, nbCells / 9] = cell[index] - '0';

                    // On enregistre cette action dans la pile
                    string lastPile = "" + nbCells % 9 + nbCells / 9 + grille[nbCells % 9, nbCells / 9];
                    // Si la dernière action a été faite sur la case actuelle, on l'enlève
                    if (pile.Last<string>()[0] - '0' == nbCells % 9 && pile.Last<string>()[1] - '0' == nbCells / 9)
                    {
                        lastPile = pile.Last<string>() + grille[nbCells % 9, nbCells / 9];
                        pile.RemoveAt(pile.Count - 1);
                    }
                    pile.Add(lastPile);

                    // On passe à la case suivante
                    nbCells++;
                }
            }
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

        public int[,] removeCells()
        {
            // Grille vidée
            int[,] grid = grille;

            // Toutes les cases qui n'ont pas encore été traitées
            List<int> possibleToRemove = new List<int>();
            for (int i = 0; i < 81; i++)
                possibleToRemove.Add(i);

            while (possibleToRemove.Count > 0)
            {
                // On prend une case au hasard parmi celles qu'il est possible de traiter
                int indexNb = Random.Shared.Next(0, possibleToRemove.Count);
                int nb = possibleToRemove[indexNb];

                // On sauvegarde sa valeur si jamais
                int saveValue = grid[nb % 9, nb / 9];
                // On l'enlève du tableau
                grid[nb % 9, nb / 9] = 0;

                // On récupère le nombre de possibilités de valeur sur la case
                int nbPoss = GetPossibleValues(grid, nb).Length;

                // Si il y a plus d'une possibilité : On ne peut pas enlever la case
                if (nbPoss > 1)
                {
                    // On remet sa valeur dans la grille
                    grid[nb % 9, nb / 9] = saveValue;
                }

                // On supprime cette case des cases non traitées
                possibleToRemove.RemoveAt(indexNb);
            }

            return grid;
        }
    }
}