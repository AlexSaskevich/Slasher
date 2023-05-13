using Source.Interfaces;
using Source.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views.SkillViews.CooldownViews
{
    public class SkillCooldownMobileView : SkillView
    {
        protected const float MaxFillAmount = 1f;

        [SerializeField] protected Button Button;

        protected Ultimate Ultimate { get; private set; }
        protected Buff Buff { get; private set; }
        protected Roll Roll { get; private set; }

        protected override void Start()
        {
            base.Start();

            ImageToFill.fillAmount = MaxFillAmount;
        }

        public override void Init(Skill skill, IInputSource inputSource, Ultimate ultimate, Buff buff, Roll roll)
        {
            base.Init(skill, inputSource, ultimate, buff, roll);

            Ultimate = ultimate;
            Buff = buff;
            Roll = roll;
        }

        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown, MaxFillAmount);
        }
    }
}