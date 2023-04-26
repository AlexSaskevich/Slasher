using Source.GameLogic.Timers;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class TimerListener : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        
        public SecondGameModeTimer SecondGameModeTimer { get; private set; } = new();

        private void OnEnable()
        {
            _playerHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        private void OnDied()
        {
            if (SecondGameModeTimer.IsTimeHighest())
                GameProgressSaver.SetTime(SecondGameModeTimer.ScoreText);
        }
    }
}