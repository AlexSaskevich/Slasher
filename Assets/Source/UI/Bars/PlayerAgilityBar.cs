using Source.Interfaces;
using Source.Player;

namespace Source.UI.Bars
{
    public sealed class PlayerAgilityBar : Bar
    {
        private PlayerAgility _playerAgility;

        private void OnDisable()
        {
            _playerAgility.AgilityChanged -= OnValueChanged;
        }

        public override void Init(PlayerHealth playerHealth, IUpgradeable upgradeable = null)
        {
            base.Init(playerHealth, upgradeable);

            if (upgradeable == null)
                return;

            _playerAgility = (PlayerAgility)upgradeable;
            _playerAgility.AgilityChanged += OnValueChanged;
        }

        protected override void OnValueChanged()
        {
            var targetValue = _playerAgility.CurrentAgility / _playerAgility.MaxValue;

            StartChangeFillAmount(targetValue);
        }
    }
}