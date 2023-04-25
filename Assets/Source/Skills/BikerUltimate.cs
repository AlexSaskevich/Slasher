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
    public sealed class BikerUltimate : Ultimate
    {
        [SerializeField] private PlayerWeapon _weapon;
        [SerializeField] private float _duration;

        private Coroutine _coroutine;
        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;
        private PlayerCombo _playerCombo;

        private void Awake()
        {
            Initialization();
            Animator = GetComponent<Animator>();
            PlayerMana = GetComponent<PlayerMana>();
            _inputSwitcher = GetComponent<InputSwitcher>();
            _playerCombo = GetComponent<PlayerCombo>();
        }

        private void Start()
        {
            _inputSource = _inputSwitcher.InputSource;
        }

        private void Update()
        {
            if (_weapon.IsBuffed == false)
                return;

            if (ElapsedTime <= Cooldown - _duration)
                _weapon.RemoveModifier(_weapon.MaxDamage);
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
            _weapon.AddModifier(_weapon.MaxDamage);
            base.TryActivate();
        }
    }
}