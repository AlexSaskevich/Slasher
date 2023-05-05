using Source.Skills;

namespace Source.UI.Views.SkillViews
{
    public sealed class UltimateEffectView : SkillView
    {
        protected override void OnStarted()
        {
            StartFillImage(Skill.Duration);
        }
    }
}