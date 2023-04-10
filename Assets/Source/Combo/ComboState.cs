using Source.Constants;

namespace Source.Combo
{
    public sealed class ComboState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Hit2);
        }

        public override void Update(PlayerCombo playerCombo)
        {
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Hit2) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new MoveState());

            if (playerCombo.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime))
                playerCombo.SwitchState(new FinishState());
        }
    }
}