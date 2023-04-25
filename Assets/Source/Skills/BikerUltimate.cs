using Source.Combo;
using Source.Constants;
using Source.InputSource;
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
            _weapon.AddModifier(_weapon.MaxDamage);
            base.TryActivate();
        }
    }
}