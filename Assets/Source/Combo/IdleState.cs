namespace Source.Combo
{
    public sealed class IdleState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
        }

        public override void Update(PlayerCombo playerCombo)
        {
            if (playerCombo.IsAttackButtonClicked)
                playerCombo.SwitchState(new EntryState());
        }

        public override void Exit(PlayerCombo playerCombo)
        {
        }
    }
}