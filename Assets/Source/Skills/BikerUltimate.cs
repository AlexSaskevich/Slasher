using Source.Combo;
using Source.Constants;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    public sealed class BikerUltimate : Ultimate
    {
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private float _duration;

        private Coroutine _coroutine;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (_playerWeapon.IsBuffed == false)
                return;

            if (ElapsedTime <= Cooldown - _duration)
                _playerWeapon.RemoveModifier(_playerWeapon.MaxDamage);
        }

        public override void TryActivate()
        {
            if (ElapsedTime < Cooldown)
                return;

            if (PlayerMana.CurrentMana < Cost)
                return;

            if (PlayerCombo.CurrentState is MoveState == false)
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
            InputSource.Disable();
            yield return new WaitUntil(() => CheckCurrentAnimationEnd());
            InputSource.Enable();
            _playerWeapon.AddModifier(_playerWeapon.MaxDamage);
            base.TryActivate();
        }
    }
}