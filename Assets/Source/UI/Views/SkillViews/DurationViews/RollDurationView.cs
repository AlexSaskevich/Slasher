namespace Source.UI.Views.SkillViews.DuarationViews
{
    public sealed class RollDurationView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}