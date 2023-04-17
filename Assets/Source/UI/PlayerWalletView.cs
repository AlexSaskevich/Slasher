using Source.Player;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class PlayerWalletView : MonoBehaviour
    {
        private TMP_Text _money;

        public PlayerWallet PlayerWallet { get; private set; } = new();

        private void Awake()
        {
            _money = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            PlayerWallet.MoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            PlayerWallet.MoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            _money.text = money.ToString();
        }
    }
}