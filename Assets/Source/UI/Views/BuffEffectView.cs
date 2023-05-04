using Source.Skills;

namespace Source.UI.Views
{
    public sealed class BuffEffectView : SkillView
    {
        private Buff _buff;

        protected override void Start()
        {
            base.Start();
            Image.fillAmount = MaxFillAmount;
        }

        protected override void OnStarted()
        {
            Show();
            StartFillImage(Buff.Duration);
        }
    }
}