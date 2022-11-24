using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    internal class Leaderboard
    {
        List<Score> scores;

        public Leaderboard()
        {
            scores = new List<Score>();
        }

        public void AddScore(Score score)
        {
            scores.Add(score);
            scores.Sort();
        }

        public void AddScore(string username, int time, int nbHelp)
        {
            Score score = new Score(username, time, nbHelp);
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
    }
}
