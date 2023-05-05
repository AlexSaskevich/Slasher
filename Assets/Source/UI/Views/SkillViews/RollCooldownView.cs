namespace Source.UI.Views.SkillViews
{
    public sealed class RollCooldownView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown);
        }
    }
}