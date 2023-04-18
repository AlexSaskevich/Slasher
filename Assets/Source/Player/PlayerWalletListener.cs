using Source.GameLogic;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerWalletListener : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        
        public PlayerWallet PlayerWallet { get; private set; }

        private void Awake()
        {
            PlayerWallet = new PlayerWallet(GameProgressSaver.GetMoney());
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
            GameProgressSaver.SetMoney(PlayerWallet.CurrentMoney);
        }
    }
}