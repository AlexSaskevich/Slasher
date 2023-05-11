namespace Source.UI.Views.SkillViews.DuarationViews
{
    public sealed class BuffDurationView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}