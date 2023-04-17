using Source.Player;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class PlayerWalletView : MonoBehaviour
    {
        private TMP_Text _money;
        
        private readonly PlayerWallet _playerWallet = new();

        private void Awake()
        {
            _money = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _playerWallet.MoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            _playerWallet.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            _money.text = money.ToString();
        }
    }
}