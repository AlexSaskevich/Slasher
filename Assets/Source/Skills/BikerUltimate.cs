using Source.Combo;
using Source.Constants;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    public sealed class BikerUltimate : Ultimate
    {
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
            Animator.SetBool(AnimationConstants.IsUltimate, true);
            IsActive = true;
            base.TryActivate();
            yield return new WaitUntil(() => ElapsedTime <= Cooldown - _duration);
            Animator.SetBool(AnimationConstants.IsUltimate, false);
            IsActive = false;
        }
    }
}