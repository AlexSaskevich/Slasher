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

        public virtual bool IsCurrentAnimationName(Animator animator, string name)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
        }

        public virtual void Exit(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Hurt);
            playerCombo.SwitchState(new MoveState());
        }

        public abstract void Enter(PlayerCombo playerCombo);

        public abstract void Update(PlayerCombo playerCombo);
    }
}