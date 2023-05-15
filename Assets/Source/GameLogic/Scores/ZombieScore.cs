using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Scores
{
    [RequireComponent(typeof(PlayerHealth))]
    public sealed class ZombieScore : Score
    {
        private PlayerHealth _playerHealth;
        
        protected override void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            TrySetHighestScore(GameProgressSaver.GetZombieScore());
            base.Awake();
        }
        
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
            if (CurrentScore > HighestScore)
                GameProgressSaver.SetZombieScore(CurrentScore);
        }
    }
}