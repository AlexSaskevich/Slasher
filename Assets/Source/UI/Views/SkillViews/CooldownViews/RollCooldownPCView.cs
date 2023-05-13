using UnityEngine;

namespace Source.UI.Views.SkillViews.CooldownViews
{
    public sealed class RollCooldownPCView : SkillCooldownPCView
    {
        private void Update()
        {
            Icon.color = (Skill.CanUsed && !Ultimate.IsActive && !Buff.IsActive) ? StartColor : Color.gray;
        }
    }
}