using System;

namespace Source.GameLogic
{
    public sealed class ZombieScore
    {
        public ZombieScore(int highestScore)
        {
            HighestScore = highestScore;
            Score = 0;
        }

        public event Action<int> ScoreChanged;
        
        public int Score { get; private set; }
        public int HighestScore { get; private set; }
        
        public void SetScore(int newScore)
        {
            if (Score >= newScore)
                return;

            Score = newScore;
            ScoreChanged?.Invoke(Score);
        }
    }
}