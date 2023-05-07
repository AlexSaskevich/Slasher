using System.Collections;
using Source.Player;
using Source.Yandex;
using TMPro;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class MoneyButton : UIButton
    {
        [SerializeField] private int _money;
        [SerializeField] private AdShower _adShower;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private float _delay;

        private PlayerWallet _playerWallet;
        private Coroutine _coroutine;
        private bool _isLocked;

        protected override void OnEnable()
        {
            base.OnEnable();

            _moneyText.text = $"+{_money}";

            if (_isLocked)
                Button.interactable = false;
        }

        public void Init(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }
        
        protected override void OnButtonClick()
        {
            GiveMoney();
        }

        private void GiveMoney()
        {
            _playerWallet.TryTakeMoney(_money);

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitTime());
            
            _adShower.Show();
        }

        private IEnumerator WaitTime()
        {
            Button.interactable = false;
            _isLocked = true;
            
            yield return new WaitForSeconds(_delay);

            Button.interactable = true;
            _isLocked = false;
        }
    }
}