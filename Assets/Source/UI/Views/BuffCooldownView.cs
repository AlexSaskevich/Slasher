namespace Source.UI.Views
{
    public sealed class BuffCooldownView : SkillView
    {
        protected override void Start()
        {
            base.Start();
            Image.fillAmount = MinFillAmount;
        }

        protected override void OnStarted()
        {
            StartFillImage(Buff.Cooldown);
        }
    }
}