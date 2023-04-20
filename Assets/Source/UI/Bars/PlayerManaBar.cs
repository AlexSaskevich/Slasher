using Source.Player;
using UnityEngine;

namespace Source.UI.Bars
{
    public class PlayerManaBar : Bar
    {
        [SerializeField] private PlayerMana _playerMana;
        
        private void Awake()
        {
            Initialize(_playerMana.MaxMana);
        }

        private void OnEnable()
        {
            _playerMana.ManaChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _playerMana.ManaChanged -= OnValueChanged;
        }

        protected override void OnValueChanged()
        {
            var targetValue = _playerMana.CurrentMana / _playerMana.MaxMana;

            StartChangeFillAmount(targetValue);
        }
    }
}