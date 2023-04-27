using Source.Player;
using UnityEngine;

namespace Source.UI.Bars
{
    public sealed class PlayerHealthBar : Bar
    {
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            Initialize(_playerHealth.MaxHealth);
        }

        private void OnEnable()
        {
            _playerHealth.HealthChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _playerHealth.HealthChanged -= OnValueChanged;
        }

        public void Init(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void OnValueChanged()
        {
            var targetValue = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;

            StartChangeFillAmount(targetValue);
        }
    }
}