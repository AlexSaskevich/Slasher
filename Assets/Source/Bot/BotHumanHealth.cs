namespace Source.Bot
{
    public sealed class BotHumanHealth : BotHealth
    {
        protected override void Die()
        {
            BotTarget.ClearTargets();
        }
    }
}