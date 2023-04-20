using Source.Player;
using UnityEngine;

namespace Source.UI.Bars
{
    public sealed class PlayerHealthBar : Bar
    {
        [SerializeField] private PlayerHealth _playerHealth;

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

        protected override void OnValueChanged()
        {
            var targetValue = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;

            StartChangeFillAmount(targetValue);
        }
    }
}