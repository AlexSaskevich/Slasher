namespace Source.UI.Views.SkillViews.DurationViews
{
    public sealed class UltimateDurationView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}