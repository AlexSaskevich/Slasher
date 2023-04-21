using Source.Player;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class ZombieScoreListener : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;

        public ZombieScore ZombieScore { get; private set; }

        private void Awake()
        {
            ZombieScore = new ZombieScore(GameProgressSaver.GetZombieScore());
            
            print(ZombieScore);
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
            if (ZombieScore.Score > ZombieScore.HighestScore)
                GameProgressSaver.SetZombieScore(ZombieScore.Score);
        }
    }
}