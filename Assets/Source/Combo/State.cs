using Source.Constants;
using Source.Interfaces;
using UnityEngine;

namespace Source.Combo
{
    public abstract class State
    {
        protected virtual bool CheckCurrentAnimationEnd(Animator animator, float animationEndTime = AnimationConstants.EndAnimationTime, int layerIndex = AnimationConstants.BaseLayer)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime > animationEndTime;
        }

        protected virtual bool IsCurrentAnimationName(Animator animator, string name, int layerIndex = AnimationConstants.BaseLayer)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(name);
        }

        protected virtual bool IsEnoughAgility(PlayerCombo playerCombo, int hitCount)
        {
            return playerCombo.PlayerAgility.CurrentAgility >= playerCombo.AgilityPerHit * hitCount;
        }

        public virtual void Exit(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Hurt);
            playerCombo.SwitchState(new MoveState());
        }

        public abstract void Enter(PlayerCombo playerCombo);

        public abstract void Update(PlayerCombo playerCombo, IInputSource inputSource);
    }
}