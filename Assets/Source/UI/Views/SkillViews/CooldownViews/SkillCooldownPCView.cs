using Source.InputSource;
using Source.Interfaces;
using Source.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views.SkillViews.CooldownViews
{
    public class SkillCooldownPCView : SkillView
    {
        [SerializeField] private Image _icon;

        private Color _startColor;

        protected override void Start()
        {
            base.Start();
            _startColor = _icon.color;
        }

        private void Update()
        {
            _icon.color = Skill.CanUsed ? _startColor : Color.gray;
        }

        public override void Init(Skill skill, IInputSource inputSource, Ultimate ultimate, Buff buff, Roll roll)
        {
            base.Init(skill, inputSource, ultimate, buff, roll);

            if (inputSource is UIInput)
                gameObject.SetActive(false);
        }

        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown);
        }
    }
}