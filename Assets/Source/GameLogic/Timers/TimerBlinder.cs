using System;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Timers
{
    public sealed class TimerBlinder : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private bool _isPlayerDied;

        public event Action Initialized;

        public SecondGameModeTimer SecondGameModeTimer { get; private set; }

        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        public void Init(float delay, PlayerHealth playerHealth)
        {
            SecondGameModeTimer = new SecondGameModeTimer((int)delay, GameProgressSaver.GetTime());
            
            _playerHealth = playerHealth;
            _playerHealth.Died += OnDied;
            
            Initialized?.Invoke();
        }

        private void Update()
        {
            if (_isPlayerDied == false)
                SecondGameModeTimer.Update(Time.deltaTime);
        }

        public void SetPlayerAlive()
        {
            _isPlayerDied = false;
        }
        
        private void OnDied()
        {
            _isPlayerDied = true;
            
            if (SecondGameModeTimer.IsTimeHighest() == false) 
                return;
            
            SecondGameModeTimer.SetHighestScore();
            
            GameProgressSaver.SetTime(SecondGameModeTimer.HighestScore);
            GameProgressSaver.SetTimeText(Timer.ScoreText);
        }
    }
}