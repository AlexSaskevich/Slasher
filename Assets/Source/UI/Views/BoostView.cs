using System.Collections.Generic;
using Source.GameLogic.Boosters;
using Source.Player;
using Source.UI.Buttons.UIButtons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views
{
    [RequireComponent(typeof(BoostBlinder),typeof(Button))]
    public sealed class BoostView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _characteristic;
        [SerializeField] private BuyCharacterButton _buyCharacterButton;
        [SerializeField] private TMP_Text _price;

        private IEnumerable<PlayerCharacter> _playerCharacters;
        private BoostBlinder _boostBlinder;
        private Button _button;

        private void Awake()
        {
            _boostBlinder = GetComponent<BoostBlinder>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _buyCharacterButton.CharacterSet += OnCharacterSet;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _buyCharacterButton.CharacterSet -= OnCharacterSet;
        }

        private void Start()
        {
            Activate();
        }
        
        private void OnCharacterSet()
        {
            Activate();
        }

        private void OnClick()
        {
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
            _characteristic.text = _boostBlinder.Boost.IsMaxLevel()
                ? $"{_boostBlinder.Boost.Upgradeable.MaxValue}"
                : $"{_boostBlinder.Boost.Upgradeable.MaxValue} / +{_boostBlinder.Boost.IncreasedValue}";
        }

        private void ShowPrice()
        {
            _price.text = _boostBlinder.Boost.IsMaxLevel() ? null : _boostBlinder.Boost.Price.ToString();
        }

        private void ShowLevel()
        {
            _level.text = _boostBlinder.Boost.Level.ToString();
        }

        private void TryTurnOff()
        {
            if (_boostBlinder.Boost.IsMaxLevel())
                _button.interactable = false;
        }
    }
}