using Source.Interfaces;
using Source.Player;

namespace Source.UI.Bars
{
    public sealed class PlayerHealthBar : Bar
    {
        private void OnDisable()
        {
            PlayerHealth.HealthChanged -= OnValueChanged;
        }

        public override void Init(PlayerHealth playerHealth, IUpgradeable upgradeable = null)
        {
            base.Init(playerHealth);
            PlayerHealth.HealthChanged += OnValueChanged;
        }

        protected override void OnValueChanged()
        {
            var targetValue = PlayerHealth.CurrentHealth / PlayerHealth.MaxHealth;

            StartChangeFillAmount(targetValue);
        }
    }
}