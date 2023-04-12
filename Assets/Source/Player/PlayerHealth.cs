using Source.GameLogic;

namespace Source.Player
{
    public sealed class PlayerHealth : Health
    {
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        protected override void Die()
        {
            _playerInput.enabled = false;
        }
    }
}