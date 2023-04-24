using Source.Combo;
using Source.Constants;
using Source.Interfaces;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(Animator), typeof(PlayerMana), typeof(PlayerCombo))]
    public sealed class Buff : Skill
    {
        [SerializeField] private MonoBehaviour _buffableBehaviour;
        [SerializeField] private float _modifier;
        [SerializeField] private float _duration;

        private IBuffable _buffable;
        private PlayerMana _playerMana;
        private Animator _animator;
        private PlayerCombo _playerCombo;
        private Coroutine _coroutine;

        public bool IsActive { get; private set; }

        private void OnValidate()
        {
            if (_buffableBehaviour && _buffableBehaviour is IBuffable == false)
                Debug.LogError(nameof(_buffableBehaviour) + " needs to implement " + nameof(IBuffable));

            if (_duration > Cooldown)
                Debug.LogError(nameof(_duration) + " must be less then " + nameof(Cooldown));
        }

        private void Awake()
        {
            _buffable = (IBuffable)_buffableBehaviour;
            _playerMana = GetComponent<PlayerMana>();
            _animator = GetComponent<Animator>();
            _playerCombo = GetComponent<PlayerCombo>();
            Initialization();
        }

        private void Update()
        {
            if (_buffable.IsBuffed == false)
                return;

            if (ElapsedTime <= Cooldown - _duration)
                _buffable.RemoveModifier(_modifier);
        }

        public override void TryActivate()
        {
            if (_playerCombo.CurrentState is MoveState == false)
                return;

            if (ElapsedTime < Cooldown)
                return;

            if (_playerMana.CurrentMana < Cost)
                return;

            StartBuff();
        }

        private void StartBuff()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ActivateBuff());
        }

        private IEnumerator ActivateBuff()
        {
            _animator.SetTrigger(AnimationConstants.Buff);
            IsActive = true;
            yield return new WaitUntil(() => CheckCurrentAnimationEnd());
            IsActive = false;
            _playerMana.DecreaseMana(Cost);
            _buffable.AddModifier(_modifier);
            StartTimer();
        }

        private bool CheckCurrentAnimationEnd()
        {
            var currentAnimationName = _animator.GetCurrentAnimatorStateInfo(AnimationConstants.TopLayer).IsName(AnimationConstants.Buff);
            var isCurrentAnimationEnd = _animator.GetCurrentAnimatorStateInfo(AnimationConstants.TopLayer).normalizedTime >= AnimationConstants.EndAnimationTime;

            return currentAnimationName && isCurrentAnimationEnd;
        }
    }
}