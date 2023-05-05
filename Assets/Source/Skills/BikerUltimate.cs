using Source.Combo;
using Source.Constants;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    public sealed class BikerUltimate : Ultimate
    {
        private Coroutine _coroutine;
        private PlayerHealth _playerHealth;

        protected override void Awake()
        {
            base.Awake();
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void OnEnable()
        {
            _playerHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        public override void TryActivate()
        {
            if (ElapsedTime < Cooldown)
                return;

            if (CanUsed == false)
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
            yield return new WaitUntil(() => ElapsedTime <= Cooldown - Duration);
            Animator.SetBool(AnimationConstants.IsUltimate, false);
            IsActive = false;
        }

        private void OnDied()
        {
            StopCoroutine(_coroutine);
            Animator.SetBool(AnimationConstants.IsUltimate, false);
        }
    }
}