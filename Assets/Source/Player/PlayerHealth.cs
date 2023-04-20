using Source.GameLogic;
using Source.InputSource;
using Source.Interfaces;

namespace Source.Player
{
    public sealed class PlayerHealth : Health
    {
        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;

        private void Awake()
        {
            _inputSwitcher = GetComponent<InputSwitcher>();
        }

        protected override void Start()
        {
            base.Start();
            _inputSource = _inputSwitcher.InputSource;
        }

        protected override void Die()
        {
            _inputSource.Disable();
        }

        public override void TryTakeDamage(float damage)
        {
            if (enabled == false)
                return;

            base.TryTakeDamage(damage);
        }
    }
}