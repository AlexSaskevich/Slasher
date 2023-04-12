using Source.Constants;

namespace Source.Combo
{
    public class DeathState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Death);
        }

        public override void Update(PlayerCombo playerCombo)
        {
        }
    }
}