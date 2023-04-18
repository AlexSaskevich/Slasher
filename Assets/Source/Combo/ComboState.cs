using Source.Constants;
using Source.Interfaces;

namespace Source.Combo
{
    public sealed class ComboState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Attack2);
        }

        public override void Update(PlayerCombo playerCombo, IInputSource inputSource)
        {
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Attack2) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new MoveState());

            if (inputSource.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime) && IsEnoughAgility(playerCombo, AnimationConstants.HitCount3))
                playerCombo.SwitchState(new FinishState());
        }
    }
}