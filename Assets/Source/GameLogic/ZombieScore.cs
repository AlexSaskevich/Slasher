using System;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic
{
    [RequireComponent(typeof(PlayerHealth))]
    public sealed class ZombieScore : MonoBehaviour
    {
        private PlayerHealth _playerHealth;

        public event Action<int> ScoreChanged;

        public int Score { get; private set; }
        public int HighestScore { get; private set; }

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            HighestScore = GameProgressSaver.GetZombieScore();
            
            ScoreChanged?.Invoke(Score);
        }
        
        private void OnEnable()
        {
            _playerHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        public void SetScore(int newScore)
        {
            if (Score >= newScore)
                return;

            Score = newScore;
            ScoreChanged?.Invoke(Score);
        }
        
        private void OnDied()
        {
            if (Score > HighestScore)
                GameProgressSaver.SetZombieScore(Score);
        }
    }
}