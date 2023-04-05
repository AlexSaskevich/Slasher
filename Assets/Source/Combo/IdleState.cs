using UnityEngine;

namespace Source.Combo
{
    public sealed class IdleState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
        }

        public override void Update(PlayerCombo playerCombo)
        {
            if (Input.GetMouseButtonDown(0))
                playerCombo.SwitchState(new EntryState());
        }

        public override void Exit(PlayerCombo playerCombo)
        {
            throw new System.NotImplementedException();
        }

        public override bool GetCurrentAnimationName(Animator animator)
        {
            throw new System.NotImplementedException();
        }
    }
}