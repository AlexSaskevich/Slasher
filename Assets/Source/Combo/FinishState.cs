using Source.Constants;

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
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Hit3) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new IdleState());

            if (playerCombo.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime))
                playerCombo.SwitchState(new EntryState());
        }

        public override void Exit(PlayerCombo playerCombo)
        {
        }
    }
}