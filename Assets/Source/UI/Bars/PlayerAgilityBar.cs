using Source.Player;
using UnityEngine;

namespace Source.UI.Bars
{
    public sealed class PlayerAgilityBar : Bar
    {
        private PlayerAgility _playerAgility;

        private void Awake()
        {
            Initialize(_playerAgility.MaxAgility);
        }

        private void OnEnable()
        {
            _playerAgility.AgilityChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _playerAgility.AgilityChanged -= OnValueChanged;
        }

        public void Init(PlayerAgility playerAgility)
        {
            _playerAgility = playerAgility;
        }
        
        protected override void OnValueChanged()
        {
            var targetValue = _playerAgility.CurrentAgility / _playerAgility.MaxAgility;

            StartChangeFillAmount(targetValue);
        }
    }
}