using Source.Combo;
using Source.Constants;
using Source.InputSource;
using Source.Interfaces;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(Animator), typeof(PlayerMana), typeof(InputSwitcher))]
    public sealed class MedicUltimate : Ultimate
    {
        private const float Modifier = 1;

        [SerializeField] private float _duration;

        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;
        private PlayerHealth _playerHealth;
        private PlayerCombo _playerCombo;
        private Coroutine _coroutine;

        private void Awake()
        {
            Initialization();
            Animator = GetComponent<Animator>();
            PlayerMana = GetComponent<PlayerMana>();
            _inputSwitcher = GetComponent<InputSwitcher>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerCombo = GetComponent<PlayerCombo>();
        }

        private void Start()
        {
            _inputSource = _inputSwitcher.InputSource;
        }

        private void Update()
        {
            if (_playerHealth.ChanceAvoidDamage == 0)
                return;

            if (ElapsedTime <= Cooldown - _duration)
                _playerHealth.RemoveModifier(Modifier);
        }

        public override void TryActivate()
        {
            if (ElapsedTime < Cooldown)
                return;

            if (PlayerMana.CurrentMana < Cost)
                return;

            if (_playerCombo.CurrentState is MoveState == false)
                return;

            StartUltimate();
        }

        private void StartUltimate()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ActivateUltimate());
        }

        private IEnumerator ActivateUltimate()
        {
            Animator.SetTrigger(AnimationConstants.Ultimate);
            _inputSource.Disable();
            yield return new WaitUntil(() => CheckCurrentAnimationEnd());
            _inputSource.Enable();
            _playerHealth.AddModifier(Modifier);
            base.TryActivate();
        }
    }
}