using Source.Player;

namespace Source.UI.Bars
{
    public sealed class PlayerHealthBar : Bar
    {
        private PlayerHealth _playerHealth;

        private void OnDisable()
        {
            _playerHealth.HealthChanged -= OnValueChanged;
        }

        public void Init(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.HealthChanged += OnValueChanged;
        }

        protected override void OnValueChanged()
        {
            var targetValue = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;

            StartChangeFillAmount(targetValue);
        }
    }
}