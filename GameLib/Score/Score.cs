using System;
namespace GameLib
{
    public class GameScore
    {
        public int Score { get; private set; }
        public int Highscore { get; private set; }

        public GameScore(int score = 0, int highscore = 0)
        {
            Score = score;
            Highscore = highscore;
        }

        public void Add(int score)
        {
            Score += score;

            if (Score > Highscore)
            {
                Highscore = Score;
            }
        }

        public void Clear()
        {
            Score = 0;
        }

        public void ClearHighscore()
        {
            Highscore = 0;
        }
    }
}
