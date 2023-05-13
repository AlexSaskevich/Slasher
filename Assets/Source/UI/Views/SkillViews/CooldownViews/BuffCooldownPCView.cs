using Source.Skills;
using UnityEngine;

namespace Source.UI.Views.SkillViews.CooldownViews
{
    public sealed class BuffCooldownPCView : SkillCooldownPCView
    {
        private void Update()
        {
            Icon.color = (Skill.CanUsed && !Ultimate.IsActive && !Roll.IsActive) ? StartColor : Color.gray;
        }
    }
}