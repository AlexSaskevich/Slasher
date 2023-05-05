using Source.Combo;
using Source.Constants;
using Source.Enums;
using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerAgility))]
    public sealed class Roll : Skill
    {
        private PlayerHealth _playerHealth;
        private PlayerAgility _playerAgility;

        public override bool CanUsed { get => _playerAgility.CurrentAgility >= Cost; }
        public bool IsActive { get; private set; }

        private void OnValidate()
        {
            if (Duration != 0)
                Duration = 0;
        }

        protected override void Awake()
        {
            base.Awake();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerAgility = GetComponent<PlayerAgility>();
        }

        protected  override void Start()
        {
            base.Start();

            var player = GetComponent<PlayerCharacter>().PlayerCharacterName;
            Duration = player == PlayerCharacterName.Ninja ? AnimationConstants.RollWithJumpDuration : AnimationConstants.RollDuration;
        }

        public override void TryActivate()
        {
            if (Animator.GetBool(AnimationConstants.IsRunning) == false)
                return;

            if (PlayerCombo.CurrentState is MoveState == false)
                return;

            if (ElapsedTime < Cooldown)
                return;

            if (CanUsed == false)
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