using Source.InputSource;
using Source.Interfaces;
using Source.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views.SkillViews.CooldownViews
{
    public class SkillCooldownPCView : SkillView
    {
        [field: SerializeField] public Image Icon { get; private set; }
        protected Color StartColor { get; private set; }
        protected Ultimate Ultimate { get; private set; }
        protected Buff Buff { get; private set; }
        protected Roll Roll { get; private set; }

        protected override void Start()
        {
            base.Start();
            StartColor = Icon.color;
        }

        public override void Init(Skill skill, IInputSource inputSource, Ultimate ultimate, Buff buff, Roll roll)
        {
            base.Init(skill, inputSource, ultimate, buff, roll);

            Ultimate = ultimate;
            Buff = buff;
            Roll = roll;

            if (inputSource is UIInput)
                gameObject.SetActive(false);
        }

        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown);
        }
    }
}