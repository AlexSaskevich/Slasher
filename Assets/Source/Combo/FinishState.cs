using Source.Constants;
using Source.Interfaces;

namespace Source.Combo
{
    public sealed class FinishState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Attack3);
        }

        public override void Update(PlayerCombo playerCombo, IInputSource inputSource)
        {
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Attack3) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new MoveState());

            if (inputSource.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator) && IsEnoughAgility(playerCombo, AnimationConstants.HitCount1))
                playerCombo.SwitchState(new EntryState());
        }
    }
}