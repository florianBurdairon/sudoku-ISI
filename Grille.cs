﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public partial class Grille
    {
        // Grille complète
        public int[,] grille { get; }

        public Grille()
        {
            grille = new int[9, 9];
            GenerateGrid();
            //grille = removeCells();
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
                    if (!(ox == x && oy == y)) // Pas la case qu'on regarde
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