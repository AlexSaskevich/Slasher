using Source.Combo;
using Source.Constants;
using Source.Interfaces;
using Source.Player;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerMana))]
    public sealed class Buff : Skill
    {
        [SerializeField] private MonoBehaviour _buffableBehaviour;
        [SerializeField] private float _modifier;

        private IBuffable _buffable;
        private PlayerMana _playerMana;
        private Coroutine _coroutine;

        public override bool CanUsed { get => _playerMana.CurrentMana >= Cost; }
        public bool IsActive { get; private set; }

        private void OnValidate()
        {
            if (_buffableBehaviour && _buffableBehaviour is IBuffable == false)
                Debug.LogError(nameof(_buffableBehaviour) + " needs to implement " + nameof(IBuffable));

            if (Duration > Cooldown)
                Debug.LogError(nameof(Duration) + " must be less then " + nameof(Cooldown));
        }

        protected override void Awake()
        {
            base.Awake();
            _buffable = (IBuffable)_buffableBehaviour;
            _playerMana = GetComponent<PlayerMana>();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (_buffable.IsBuffed == false)
                return;

            if (ElapsedTime <= Cooldown - Duration)
                _buffable.RemoveModifier(_modifier);
        }

        public override void TryActivate()
        {
            if (PlayerCombo.CurrentState is MoveState == false)
                return;

            if (ElapsedTime < Cooldown)
                return;

            if (CanUsed == false)
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
            Animator.SetTrigger(AnimationConstants.Buff);
            IsActive = true;
            yield return new WaitUntil(() => CheckCurrentAnimationEnd());
            IsActive = false;
            _playerMana.DecreaseMana(Cost);
            _buffable.AddModifier(_modifier);
            StartTimer();
        }

        private bool CheckCurrentAnimationEnd()
        {
            var currentAnimationName = Animator.GetCurrentAnimatorStateInfo(AnimationConstants.TopLayer).IsName(AnimationConstants.Buff);
            var isCurrentAnimationEnd = Animator.GetCurrentAnimatorStateInfo(AnimationConstants.TopLayer).normalizedTime >= AnimationConstants.EndAnimationTime;

            return currentAnimationName && isCurrentAnimationEnd;
        }
    }
}