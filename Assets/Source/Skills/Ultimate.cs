using Source.Constants;
using Source.InputSource;
using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(PlayerMana), typeof(InputSwitcher))]
    public abstract class Ultimate : Skill
    {
        [field: SerializeField] public float Duration { get; private set; }
        public bool IsActive { get; protected set; }
        protected PlayerMana PlayerMana { get; private set; }
        protected InputSwitcher InputSwitcher { get; private set; }
        protected IInputSource InputSource { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            PlayerMana = GetComponent<PlayerMana>();
            InputSwitcher = GetComponent<InputSwitcher>();
        }

        protected override void Start()
        {
            base.Start();
            InputSource = InputSwitcher.InputSource;
        }

        public override void TryActivate()
        {
            PlayerMana.DecreaseMana(Cost);
            StartTimer();
        }

        protected virtual bool CheckCurrentAnimationEnd()
        {
            var currentAnimationName = Animator.GetCurrentAnimatorStateInfo(AnimationConstants.BaseLayer).IsName(AnimationConstants.Ultimate);
            var isCurrentAnimationEnd = Animator.GetCurrentAnimatorStateInfo(AnimationConstants.BaseLayer).normalizedTime >= AnimationConstants.EndAnimationTime;

            return currentAnimationName && isCurrentAnimationEnd;
        }
    }
}