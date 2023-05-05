using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views.SkillViews
{
    public class SkillCooldownView : SkillView
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

        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown);
        }
    }
}