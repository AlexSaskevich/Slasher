using UnityEngine;

namespace Source.UI.Views.SkillViews.CooldownViews
{
    public sealed class UltimateCooldownPCView : SkillCooldownPCView
    {
        private void Update()
        {
            Icon.color = (Skill.CanUsed && !Roll.IsActive && !Buff.IsActive) ? StartColor : Color.gray;
        }
    }
}