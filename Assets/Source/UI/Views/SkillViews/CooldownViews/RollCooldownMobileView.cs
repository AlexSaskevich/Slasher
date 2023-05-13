namespace Source.UI.Views.SkillViews.CooldownViews
{
    public sealed class RollCooldownMobileView : SkillCooldownMobileView
    {
        private void Update()
        {
            Button.interactable = Skill.CanUsed && ImageToFill.fillAmount == MaxFillAmount && !Ultimate.IsActive && !Buff.IsActive;
        }
    }
}