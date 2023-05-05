namespace Source.UI.Views.SkillViews
{
    public sealed class BuffEffectView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}