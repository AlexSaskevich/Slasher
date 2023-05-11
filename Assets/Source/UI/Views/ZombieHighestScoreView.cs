namespace Source.UI.Views
{
    public sealed class ZombieHighestScoreView : ZombieScoreView
    {
        protected override void Init()
        {
            Score.text = ZombieScore.HighestScore.ToString();
        }
    }
}