using Source.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views.SkillViews.CooldownViews
{
    public class SkillCooldownMobileView : SkillView
    {
        private const float MaxFillAmount = 1f;

        [SerializeField] private Button _button;

        protected override void Start()
        {
            base.Start();

            ImageToFill.fillAmount = MaxFillAmount;
        }

        private void Update()
        {
            _button.interactable = Skill.CanUsed && ImageToFill.fillAmount == MaxFillAmount;
        }

        protected override void OnStarted()
        {
            StartFillImage(Skill.Cooldown, MaxFillAmount);
        }
    }
}