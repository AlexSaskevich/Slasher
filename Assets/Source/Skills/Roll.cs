using Source.Combo;
using Source.Constants;
using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerCombo), typeof(PlayerHealth))]
    public sealed class Roll : Skill
    {
        private PlayerInput _playerInput;
        private PlayerCombo _playerCombo;
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerCombo = GetComponent<PlayerCombo>();
            _playerHealth = GetComponent<PlayerHealth>();
            Initialization();
        }

        public override void TryActivate()
        {
            if (_playerCombo.Animator.GetBool(AnimationConstants.IsRunning) == false)
                return;

            if (_playerCombo.CurrentState is MoveState == false)
                return;

            if (ElapsedTime < Cooldown)
                return;

            if (_playerCombo.PlayerAgility.CurrentAgility < Cost)
                return;

            _playerCombo.Animator.SetTrigger(AnimationConstants.Roll);

            _playerCombo.PlayerAgility.DecreaseAgility(Cost);

            StartTimer();
        }

        public void StartRoll()
        {
            _playerInput.enabled = false;
            _playerHealth.enabled = false;
        }

        public void StopRoll()
        {
            _playerInput.enabled = true;
            _playerHealth.enabled = true;
        }
    }
}