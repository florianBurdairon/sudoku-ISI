using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace sudoku
{
    /*
     * Classe permettant d'enregistrer chaque score
     */
    [Serializable]
    internal class Score : IComparable<Score>
    {
        public string username { get; set; }
        public int time { get; set; }
        public int nbHelp { get; set; }
        public Difficulty difficulty { get; set; }

        public Score()
        {
            username = "";
            time = 0;
            nbHelp = 0;
            difficulty = Difficulty.None;
        }

        [JsonConstructor]
        public Score(string username, int time, int nbHelp, Difficulty difficulty)
        {
            this.username = username;
            this.time = time;
            this.nbHelp = nbHelp;
            this.difficulty = difficulty;
        }
        
        /*
         * Permet de récupérer l'écriture du score pour l'afficher dans le tableau des scores
         */
        public override string ToString()
        {
            string timeString = "";

            if (time / 3600 > 0)
            {
                timeString += (time / 3600).ToString() + ":";
            }
            string m = ((time % 3600) / 60).ToString();
            if (m.Length == 1)
                m = "0" + m;
            string s = (time % 60).ToString();
            if (s.Length == 1)
                s = "0" + s;

            timeString += m + ":" + s;

            string data = username + " : " + timeString;
            return data;
        }

        /*
         * Permet de comparer les scores entres eux
         */
        public int CompareTo(Score? other)
        {
            if (other is Score o)
            {
                return time.CompareTo(o.time);
            }
            else
                return 0;
        }
    }
}
