using Source.Constants;
using Source.Interfaces;
using UnityEngine;

namespace Source.Combo
{
    public sealed class EntryState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Attack1);
        }

        public override void Update(PlayerCombo playerCombo, IInputSource inputSource)
        {
            if (CheckAttackAnimationFromIdle(playerCombo.Animator))
                playerCombo.PlayerMovement.SetSpeed(0);
            else
                playerCombo.PlayerMovement.SetSpeed(playerCombo.PlayerMovement.FinalSpeed);

            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Attack1) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new MoveState());

            if (inputSource.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime) && IsEnoughAgility(playerCombo, AnimationConstants.HitCount2))
                playerCombo.SwitchState(new ComboState());
        }

        protected override bool CheckCurrentAnimationEnd(Animator animator, float animationEndTime = AnimationConstants.EndAnimationTime, int layerIndex = AnimationConstants.TopLayer)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime > animationEndTime;
        }

        protected override bool IsCurrentAnimationName(Animator animator, string name, int layerIndex = AnimationConstants.TopLayer)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(name);
        }

        private bool CheckAttackAnimationFromIdle(Animator animator)
        {
            var isIdleAnimation = IsCurrentAnimationName(animator, AnimationConstants.Idle, AnimationConstants.BaseLayer);
            var isAttackAnimationEnd = CheckCurrentAnimationEnd(animator, AnimationConstants.IdleEndAnimationTime, AnimationConstants.BaseLayer) == false;

            return isIdleAnimation & isAttackAnimationEnd;
        }
    }
}