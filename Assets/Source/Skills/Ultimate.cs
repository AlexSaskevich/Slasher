using Source.Combo;
using Source.Constants;
using Source.InputSource;
using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    public abstract class Ultimate : Skill
    {
        public PlayerMana PlayerMana { get; protected set; }
        public Animator Animator { get; protected set; }

        protected InputSwitcher InputSwitcher { get; private set; }
        protected IInputSource InputSource { get; private set; }
        protected PlayerCombo PlayerCombo { get; private set; }

        protected virtual void Awake()
        {
            Initialization();
            Animator = GetComponent<Animator>();
            PlayerMana = GetComponent<PlayerMana>();
            InputSwitcher = GetComponent<InputSwitcher>();
            PlayerCombo = GetComponent<PlayerCombo>();
        }

        protected virtual void Start()
        {
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