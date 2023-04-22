using System;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic
{
    [RequireComponent(typeof(PlayerHealth))]
    public sealed class ZombieScore : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private int _score;

        public event Action<int> ScoreChanged;

        public int HighestScore { get; private set; }
        public int Score { get; private set; } = 0;

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            HighestScore = GameProgressSaver.GetZombieScore();
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
            if (_score >= newScore)
                return;

            _score = newScore;
            ScoreChanged?.Invoke(_score);
        }
        
        private void OnDied()
        {
            if (_score > HighestScore)
                GameProgressSaver.SetZombieScore(_score);
        }
    }
}