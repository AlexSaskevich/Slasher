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

        public void Init(PlayerMana playerMana)
        {
            _playerMana = playerMana;
            _playerMana.ManaChanged += OnValueChanged;
        }

        protected override void OnValueChanged()
        {
            var targetValue = _playerMana.CurrentValue / _playerMana.MaxValue;

            StartChangeFillAmount(targetValue);
        }
    }
}