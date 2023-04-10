using Source.Constants;
using UnityEngine;

namespace Source.Combo
{
    public sealed class EntryState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            Debug.Log("In Entry state");
            playerCombo.Animator.SetTrigger(AnimationConstants.Hit1);
        }

        public override void Update(PlayerCombo playerCombo)
        {
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Hit1) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new IdleState());

            if (playerCombo.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime))
                playerCombo.SwitchState(new ComboState());
        }

        public override void Exit(PlayerCombo playerCombo)
        {
        }
    }
}