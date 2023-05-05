namespace Source.UI.Views.SkillViews
{
    public sealed class BuffCooldownView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown);
        }
    }
}