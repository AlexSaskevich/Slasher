using System;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Timers
{
    public sealed class TimerListener : MonoBehaviour
    {
        private PlayerHealth _playerHealth;

        public event Action Initialized;

        public SecondGameModeTimer SecondGameModeTimer { get; private set; }

        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        public void Init(float delay, PlayerHealth playerHealth)
        {
            SecondGameModeTimer = new SecondGameModeTimer((int)delay);
            
            _playerHealth = playerHealth;
            _playerHealth.Died += OnDied;
            
            Initialized?.Invoke();
        }

        private void OnDied()
        {
            if (SecondGameModeTimer.IsTimeHighest())
                GameProgressSaver.SetTime(Timer.ScoreText);
        }
    }
}