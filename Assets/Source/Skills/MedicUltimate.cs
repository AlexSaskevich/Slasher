using Source.Combo;
using Source.Constants;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerHealth))]
    public sealed class MedicUltimate : Ultimate
    {
        private PlayerHealth _playerHealth;
        private Coroutine _coroutine;

        protected override void Awake()
        {
            base.Awake();
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            if (_playerHealth.ChanceAvoidDamage == 0)
                return;

            if (ElapsedTime <= Cooldown - Duration)
                _playerHealth.StopAvoidDamage();
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
            _playerHealth.StartAvoidDamage();
            base.TryActivate();
        }
    }
}