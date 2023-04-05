using Source.Constants;
using UnityEngine;

namespace Source.Combo
{
    public sealed class FinishState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Hit3);
        }

        public override void Update(PlayerCombo playerCombo)
        {
            if (CheckCurrentAnimationEnd(playerCombo.Animator) && GetCurrentAnimationName(playerCombo.Animator))
                playerCombo.SwitchState(new IdleState());

            if (Input.GetMouseButtonDown(0) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime) && GetCurrentAnimationName(playerCombo.Animator))
                playerCombo.SwitchState(new EntryState());
        }

        public override bool CheckCurrentAnimationEnd(Animator animator, float animationEndTime = AnimationConstants.EndAnimationTime)
        {
            return base.CheckCurrentAnimationEnd(animator, animationEndTime);
        }

        public override bool GetCurrentAnimationName(Animator animator)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationConstants.Hit3);
        }

        public override void Exit(PlayerCombo playerCombo)
        {
        }
    }
}