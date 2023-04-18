using Source.Constants;
using Source.Interfaces;

namespace Source.Combo
{
    public sealed class DeathState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Death);
            playerCombo.enabled = false;
        }

        public override void Update(PlayerCombo playerCombo, IInputSource inputSource)
        {
        }
    }
}