namespace Source.UI.Views.SkillViews.DurationViews
{
    public sealed class BuffDurationView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}