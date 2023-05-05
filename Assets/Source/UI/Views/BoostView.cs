using Source.GameLogic.Goods;
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
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void Start()
        {
            ShowLevel();
            ShowCharacteristic();
            TryTurnOff();
        }

        private void OnClick()
        {
            _boostBlinder.Boost.TryBuy();
            
            ShowLevel();
            ShowCharacteristic();
            TryTurnOff();
        }

        private void ShowCharacteristic()
        {
            _characteristic.text = _boostBlinder.Boost.IsMaxLevel()
                ? $"{_boostBlinder.Boost.Upgradeable.MaxValue}"
                : $"{_boostBlinder.Boost.Upgradeable.MaxValue} / +{_boostBlinder.Boost.IncreasedValue}";
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