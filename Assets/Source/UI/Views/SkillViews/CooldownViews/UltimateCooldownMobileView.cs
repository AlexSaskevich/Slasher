namespace Source.UI.Views.SkillViews.CooldownViews
{
    public sealed class UltimateCooldownMobileView : SkillCooldownMobileView
    {
        private void Update()
        {
            Button.interactable = Skill.CanUsed && ImageToFill.fillAmount == MaxFillAmount && !Buff.IsActive && !Roll.IsActive;
        }
    }
}