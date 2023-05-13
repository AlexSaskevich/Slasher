namespace Source.UI.Views.SkillViews.DurationViews
{
    public sealed class RollDurationView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}