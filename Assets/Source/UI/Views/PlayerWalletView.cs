using Source.GameLogic;
using Source.Player;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class PlayerWalletView : MonoBehaviour
    {
        private PlayerWallet _playerWallet; 
            
        private TMP_Text _money;

        private void Awake()
        {
            _money = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _playerWallet.MoneyChanged += OnMoneyChanged;
            
            ShowMoney();
        }

        private void OnDisable()
        {
            _playerWallet.MoneyChanged -= OnMoneyChanged;
        }

        private void Start()
        {
            ShowMoney();
        }

        public void Init(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }

        private void OnMoneyChanged()
        {
            ShowMoney();
        }

        private void ShowMoney()
        {
            _money.text = _playerWallet.CurrentMoney.ToString();
        }
    }
}