using Source.Constants;

namespace Source.Combo
{
    public sealed class EntryState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Attack1);
        }

        public override void Update(PlayerCombo playerCombo)
        {
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Attack1) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new MoveState());

            if (playerCombo.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime) && IsEnoughAgility(playerCombo.PlayerAgility, AnimationConstants.HitCount2))
                playerCombo.SwitchState(new ComboState());
        }
    }
}