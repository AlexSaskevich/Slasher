using Source.UI;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(BotHealth))]
    public sealed class BotReward : MonoBehaviour
    {
        [SerializeField] private int _minReward;
        [SerializeField] private int _maxReward;

        private BotHealth _botHealth;
        private PlayerWalletView _playerWalletView;
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

        public void Init(PlayerWalletView playerWalletView)
        {
            _playerWalletView = playerWalletView;
        }
        
        private void OnDied()
        {
            _playerWalletView.PlayerWallet.TryTakeMoney(_reward);
        }
    }
}