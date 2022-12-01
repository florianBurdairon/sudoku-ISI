using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sudoku
{
    /*
     * Classe de gestion du tableau des scores
     */
    [Serializable]
    internal class Leaderboard
    {
        public List<Score> scores { get; set; }

        public Leaderboard()
        {
            scores = new List<Score>();
        }

        [JsonConstructor]
        public Leaderboard(List<Score> scores)
        {
            if(scores != null)
                this.scores = scores;
            else
                this.scores = new List<Score>();
        }

        public void AddScore(Score score)
        {
            scores.Add(score);
            scores.Sort();
        }

        public void AddScore(string username, int time, int nbHelp, Difficulty difficulty)
        {
            Score score = new Score(username, time, nbHelp, difficulty);
            AddScore(score);
        }

        public Score GetBestScore()
        {
            return scores.First();
        }

        public List<Score> GetLeaderboard()
        {
            return scores;
        }

        public List<Score> GetLeaderboardWith(Difficulty difficulty)
        {
            List<Score> ret = new List<Score>();

            for (int i = 0; i < scores.Count; i++)
                if (scores[i].difficulty == difficulty)
                    ret.Add(scores[i]);

            return ret;
        }
    }
}
