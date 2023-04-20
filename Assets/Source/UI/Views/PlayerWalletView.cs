using Source.Player;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class PlayerWalletView : MonoBehaviour
    {
        [SerializeField] private PlayerWalletListener _playerWalletListener; 
            
        private TMP_Text _money;

        public PlayerWallet PlayerWallet { get; private set; }

        private void Awake()
        {
            PlayerWallet = _playerWalletListener.PlayerWallet;
            
            _money = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            PlayerWallet.MoneyChanged += OnMoneyChanged;
            
            ShowMoney();
        }

        private void OnDisable()
        {
            PlayerWallet.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            ShowMoney();
        }

        private void ShowMoney()
        {
            _money.text = PlayerWallet.CurrentMoney.ToString();
        }
    }
}