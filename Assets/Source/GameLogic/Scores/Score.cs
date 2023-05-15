using System;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Scores
{
    [RequireComponent(typeof(PlayerHealth))]
    public abstract class Score : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        
        public event Action<int> ScoreChanged;
        
        public int CurrentScore { get; private set; }
        public int HighestScore { get; private set; }

        protected virtual void Awake()
        {
            ScoreChanged?.Invoke(CurrentScore);
        }
        
        public void SetScore(int newScore)
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