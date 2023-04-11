using Source.Constants;

namespace Source.Combo
{
    public sealed class ComboState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Attack2);
        }

        public override void Update(PlayerCombo playerCombo)
        {
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Attack2) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new MoveState());

            if (playerCombo.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime) && IsEnoughAgility(playerCombo.PlayerAgility, AnimationConstants.HitCount3))
                playerCombo.SwitchState(new FinishState());
        }
    }
}