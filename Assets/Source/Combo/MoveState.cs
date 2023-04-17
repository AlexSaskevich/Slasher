using Source.Constants;
using Source.Player;
using UnityEngine;

namespace Source.Combo
{
    public sealed class MoveState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
        }

        public override void Update(PlayerCombo playerCombo, PlayerInput playerInput)
        {
            bool isRunning = playerCombo.CharacterController.velocity.magnitude > InputConstants.Epsilon;
            Animate(playerCombo.Animator, isRunning);

            if (playerInput.IsAttackButtonClicked && IsEnoughAgility(playerCombo, AnimationConstants.HitCount1))
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