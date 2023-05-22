using Source.GameLogic.Boosters;
using Source.Player;
using Source.Turntables;
using Source.UI.Buttons.UIButtons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views
{
    [RequireComponent(typeof(BoostBlinder), typeof(Button))]
    public sealed class BoostView : MonoBehaviour
    {
        private const float MaxIconAlpha = 1f;
        private const float MinIconAlpha = 0.5f;

        [SerializeField] private ButtonTurntable _buttonTurntable;
        [SerializeField] private BuyCharacterButton _buyCharacterButton;
        [SerializeField] private TMP_Text _priceValue;
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private TMP_Text _increasedValue;
        [SerializeField] private TMP_Text _maxText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image[] _levelIcons;

        private BoostBlinder _boostBlinder;
        private Button _button;
        private PlayerWallet _playerWallet;

        private void Awake()
        {
            _boostBlinder = GetComponent<BoostBlinder>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _buyCharacterButton.CharacterSet += OnCharacterSet;

            Activate();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _buyCharacterButton.CharacterSet -= OnCharacterSet;

            _playerWallet.MoneyChanged -= OnMoneyChanged;
        }

        private void Start()
        {
            Activate();
        }

        public void Init(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;

            _playerWallet.MoneyChanged += OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            Activate();
        }

        private void OnCharacterSet()
        {
            Activate();
        }

        private void OnClick()
        {
            _buttonTurntable.PlayButtonSound();

            _boostBlinder.Boost.TryBuy();
            Activate();
        }

        private void Activate()
        {
            ShowLevel();
            ShowCharacteristic();
            ShowPrice();
            TryTurnOff();
        }

        private void ShowCharacteristic()
        {
            _currentValue.text = $"{_boostBlinder.Boost.Upgradeable.MaxValue}";

            if (_boostBlinder.Boost.IsMaxLevel())
            {
                _increasedValue.text = string.Empty;
                return;
            }

            _increasedValue.text = _boostBlinder.Boost.Level == 0
                ? $"+{_boostBlinder.Boost.IncreasedValue}"
                : $"+{_boostBlinder.Boost.UIIncreasedValue}";
        }

        private void ShowPrice()
        {
            var isMaxLevel = _boostBlinder.Boost.IsMaxLevel();

            _maxText.gameObject.SetActive(isMaxLevel);
            _priceValue.gameObject.SetActive(!isMaxLevel);
            _priceText.gameObject.SetActive(!isMaxLevel);

            var price = _boostBlinder.Boost.IsMaxLevel() ? null : _boostBlinder.Boost.Price.ToString();

            _priceValue.text = price;
        }

        private void ShowLevel()
        {
            SetLevelIcons(_boostBlinder.Boost.Level);
        }

        private void SetLevelIcons(int level)
        {
            for (int i = 0; i < _levelIcons.Length; i++)
            {
                var tempColor = _levelIcons[i].color;

                tempColor.a = i <= level - 1 ? MaxIconAlpha : MinIconAlpha;

                _levelIcons[i].color = tempColor;
            }
        }

        private void TryTurnOff()
        {
            if (_boostBlinder.Boost.IsMaxLevel())
            {
                _button.interactable = false;
                return;
            }

            _button.interactable = _boostBlinder.Boost.Wallet.CurrentMoney >= _boostBlinder.Boost.Price;
        }
    }
}