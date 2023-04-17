using Source.Combo;
using Source.Constants;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerInput), typeof(PlayerCombo))]
    public sealed class BikerUltimate : Ultimate
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _duration;

        private Coroutine _coroutine;
        private PlayerMovement _playerMovement;
        private PlayerInput _playerInput;
        private PlayerCombo _playerCombo;

        private void Awake()
        {
            Initialization();
            PlayerMana = GetComponent<PlayerMana>();
            Animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
            _playerCombo = GetComponent<PlayerCombo>();
        }

        private void Update()
        {
            if (_playerMovement.IsBuffed == false && _weapon.IsBuffed == false)
                return;

            if (ElapsedTime <= Cooldown - _duration)
            {
                _playerMovement.RemoveModifier(_speedModifier);
                _weapon.RemoveModifier(_weapon.MaxDamage);
            }
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
            _playerInput.enabled = false;
            yield return new WaitUntil(() => CheckCurrentAnimationEnd());
            _playerInput.enabled = true;
            _playerMovement.AddModifier(_speedModifier);
            _weapon.AddModifier(_weapon.MaxDamage);
            base.TryActivate();
        }

        private bool CheckCurrentAnimationEnd()
        {
            var currentAnimationName = Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationConstants.Ultimate);
            var isCurrentAnimationEnd = Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= AnimationConstants.EndAnimationTime;

            return currentAnimationName && isCurrentAnimationEnd;
        }
    }
}