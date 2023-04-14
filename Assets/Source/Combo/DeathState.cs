using Source.Constants;
using Source.Player;

namespace Source.Combo
{
    public sealed class DeathState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Death);
        }

        public override void Update(PlayerCombo playerCombo, PlayerInput playerInput)
        {
        }
    }
}