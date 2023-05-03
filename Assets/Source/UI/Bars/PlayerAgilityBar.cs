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

        public void Init(PlayerAgility playerAgility)
        {
            _playerAgility = playerAgility;
            _playerAgility.AgilityChanged += OnValueChanged;
        }

        protected override void OnValueChanged()
        {
            var targetValue = _playerAgility.CurrentAgility / _playerAgility.MaxAgility;

            StartChangeFillAmount(targetValue);
        }
    }
}