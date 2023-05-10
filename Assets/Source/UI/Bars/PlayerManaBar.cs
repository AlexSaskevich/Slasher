using Source.Interfaces;
using Source.Player;

namespace Source.UI.Bars
{
    public sealed class PlayerManaBar : Bar
    {
        private PlayerMana _playerMana;

        private void OnDisable()
        {
            _playerMana.ManaChanged -= OnValueChanged;
        }

        public override void Init(PlayerHealth playerHealth, IUpgradeable upgradeable = null)
        {
            base.Init(playerHealth, upgradeable);

            if (upgradeable == null)
                return;

            _playerMana = (PlayerMana)upgradeable;
            _playerMana.ManaChanged += OnValueChanged;
        }

        protected override void OnValueChanged()
        {
            var targetValue = _playerMana.CurrentValue / _playerMana.MaxValue;

            StartChangeFillAmount(targetValue);
        }
    }
}