namespace Source.UI.Views.SkillViews
{
    public sealed class RollEffectView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}