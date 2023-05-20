using System;
using Source.Enums;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Scores
{
    [RequireComponent(typeof(PlayerHealth))]
    public abstract class Score : MonoBehaviour
    {
        [field: SerializeField] protected LeaderboardName LeaderboardName { get; private set; }

        public event Action<int> ScoreChanged;
        
        public int CurrentScore { get; private set; }
        public int HighestScore { get; private set; }

        protected virtual void Awake()
        {
            ScoreChanged?.Invoke(CurrentScore);
        }

        public virtual void SetScore(int newScore)
        {
            if (CurrentScore >= newScore)
                return;

            CurrentScore = newScore;
            ScoreChanged?.Invoke(CurrentScore);
        }
        
        protected void TrySetHighestScore(int score)
        {
            if (score < 0)
                return;

            if (HighestScore > 0 && score <= HighestScore)
                return;

            HighestScore = score;
        }
    }
}