namespace Source.UI.Views
{
    public sealed class BuffCooldownView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Buff.Cooldown);
        }
    }
}