namespace Source.UI.Views
{
    public sealed class BuffEffectView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Buff.Duration);
        }
    }
}