using Source.Constants;
using UnityEngine;

namespace Source.Combo
{
    public sealed class MoveState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
        }

        public override void Update(PlayerCombo playerCombo)
        {
            bool isRunning = playerCombo.Rigidbody.velocity.normalized.magnitude > 0;
            Animate(playerCombo.Animator, isRunning);

            if (playerCombo.IsAttackButtonClicked)
                playerCombo.SwitchState(new EntryState());
        }

        public override void Exit(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Hurt);
        }

        private void Animate(Animator animator, bool value)
        {
            animator.SetBool(AnimationConstants.IsRunning, value);
        }
    }
}