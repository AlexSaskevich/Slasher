using Source.Combo;
using Source.Constants;
using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerAgility))]
    public sealed class Roll : Skill
    {
        private PlayerHealth _playerHealth;
        private PlayerAgility _playerAgility;

        public bool IsActive { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerAgility = GetComponent<PlayerAgility>();
        }

        public override void TryActivate()
        {
            if (Animator.GetBool(AnimationConstants.IsRunning) == false)
                return;

            if (PlayerCombo.CurrentState is MoveState == false)
                return;

            if (ElapsedTime < Cooldown)
                return;

            if (_playerAgility.CurrentAgility < Cost)
                return;

            Animator.SetTrigger(AnimationConstants.Roll);

            _playerAgility.DecreaseAgility(Cost);

            StartTimer();
        }

        public void StartRoll()
        {
            IsActive = true;
            _playerHealth.enabled = false;
        }

        public void StopRoll()
        {
            IsActive = false;
            _playerHealth.enabled = true;
        }
    }
}