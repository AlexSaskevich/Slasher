using Source.Constants;
using UnityEngine;

namespace Source.Combo
{
    public abstract class State
    {
        public virtual bool CheckCurrentAnimationEnd(Animator animator, float animationEndTime = AnimationConstants.EndAnimationTime)
        {
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > animationEndTime;
        }

        public abstract void Enter(PlayerCombo playerCombo);

        public abstract void Update(PlayerCombo playerCombo);

        public abstract void Exit(PlayerCombo playerCombo);

        public abstract bool GetCurrentAnimationName(Animator animator);
    }
}