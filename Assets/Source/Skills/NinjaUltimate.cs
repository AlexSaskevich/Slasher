using Source.Combo;
using Source.Constants;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    public class NinjaUltimate : Ultimate
    {
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _damageModifier;

        private PlayerMovement _playerMovement;
        private PlayerAgility _playerAgility;
        private Coroutine _coroutine;

        protected override void Awake()
        {
            base.Awake();

            _playerMovement = GetComponent<PlayerMovement>();
            _playerAgility = GetComponent<PlayerAgility>();
        }

        private void Update()
        {
            if (_playerMovement.IsBuffed == false)
                return;

            if (ElapsedTime <= Cooldown - Duration)
            {
                _playerMovement.RemoveModifier(_speedModifier);
                _playerAgility.RemoveModifier(Mathf.Epsilon);
                _playerWeapon.RemoveModifier(_damageModifier);
            }
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
            _playerMovement.AddModifier(_speedModifier);
            _playerAgility.AddModifier(Mathf.Epsilon);
            _playerWeapon.AddModifier(_damageModifier);
            base.TryActivate();
        }
    }
}