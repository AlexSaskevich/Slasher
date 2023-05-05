using Source.Skills;

namespace Source.UI.Views.SkillViews
{
    public sealed class UltimateCooldownView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown);
        }
    }
}