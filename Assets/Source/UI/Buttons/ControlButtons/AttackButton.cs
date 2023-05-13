using Source.Combo;
using Source.Skills;

namespace Source.UI.Buttons.ControlButtons
{
    public sealed class AttackButton : ControlButton
    {
        private Ultimate _ultimate;
        private Buff _buff;
        private Roll _roll;
        private PlayerCombo _playerCombo;

        private void Update()
        {
            Button.interactable = !_ultimate.IsActive && !_roll.IsActive && !_buff.IsActive && _playerCombo.AgilityPerHit <= _playerCombo.PlayerAgility.CurrentAgility;
        }

        public void Init(Ultimate ultimate, Buff buff, Roll roll, PlayerCombo playerCombo)
        {
            _ultimate = ultimate;
            _buff = buff;
            _roll = roll;
            _playerCombo = playerCombo;
        }
    }
}