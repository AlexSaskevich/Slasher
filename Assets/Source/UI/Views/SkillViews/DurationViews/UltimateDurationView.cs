namespace Source.UI.Views.SkillViews.DuarationViews
{
    public sealed class UltimateDurationView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}