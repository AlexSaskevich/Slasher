using Source.Player;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(BotHealth))]
    public sealed class BotReward : MonoBehaviour
    {
        [SerializeField] private int _minReward;
        [SerializeField] private int _maxReward;

        private BotHealth _botHealth;
        private PlayerWallet _playerWallet;
        private int _reward;

        private void Awake()
        {
            _botHealth = GetComponent<BotHealth>();
        }

        private void OnEnable()
        {
            _botHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _botHealth.Died -= OnDied;
        }

        private void Start()
        {
            _reward = Random.Range(_minReward, _maxReward);
        }

        public void Init(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }
        
        private void OnDied()
        {
            _playerWallet.TryTakeMoney(_reward);
        }
    }
}