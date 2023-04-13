using Source.Combo;
using Source.Constants;
using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    public class Roll : Skill
    {
        private PlayerInput _playerInput;
        private PlayerCombo _playerCombo;
        private PlayerHealth _playerHealth;
        private PlayerAgility _playerAgility;

        [field: SerializeField] public float AgilityValue { get; private set; }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerCombo = GetComponent<PlayerCombo>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerAgility = GetComponent<PlayerAgility>();
            Initialization();
        }

        private void Update()
        {
            if (_playerCombo.Animator.GetBool(AnimationConstants.IsRunning) == false)
                return;

            if (_playerCombo.CurrentState is MoveState == false)
                return;

            if (ElapsedTime < Cooldown)
                return;

            if (_playerInput.IsRollButtonClicked && _playerAgility.CurrentAgility > AgilityValue)
                Activate();
        }

        protected override void Activate()
        {
            _playerCombo.Animator.SetTrigger(AnimationConstants.Roll);

            StartTimer();

            _playerAgility.DecreaseAgility(AgilityValue);
        }

        public void StartSkill()
        {
            _playerInput.enabled = false;
            _playerHealth.enabled = false;
        }

        public void StopSkill()
        {
            _playerInput.enabled = true;
            _playerHealth.enabled = true;
        }
    }
}