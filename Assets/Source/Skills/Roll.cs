using Source.Combo;
using Source.Constants;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    public class Roll : MonoBehaviour, ISkill
    {
        private PlayerInput _playerInput;
        private PlayerCombo _playerCombo;
        private PlayerHealth _playerHealth;
        private PlayerAgility _playerAgility;
        private float _elapsedTime;

        [field: SerializeField] public float AgilityValue { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }

        private Coroutine _coroutine;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerCombo = GetComponent<PlayerCombo>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerAgility = GetComponent<PlayerAgility>();
            _elapsedTime = Cooldown;
        }

        private void Update()
        {
            if (_playerCombo.Animator.GetBool(AnimationConstants.IsRunning) == false)
                return;

            if (_playerCombo.CurrentState is MoveState == false)
                return;

            if (_elapsedTime < Cooldown)
                return;

            if (_playerInput.IsRollButtonClicked && _playerAgility.CurrentAgility > AgilityValue)
                Activate();
        }

        public void Activate()
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

        private void StartTimer()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (_elapsedTime > 0)
            {
                _elapsedTime = Mathf.Clamp(_elapsedTime - Time.deltaTime, 0, float.MaxValue);
                yield return null;
            }

            _elapsedTime = Cooldown;
        }
    }
}