namespace Source.UI.Views
{
    public sealed class ZombieHighestScoreView : ZombieScoreView
    {
        private void OnEnable()
        {
            Score.text = ZombieScore.HighestScore.ToString();
        }
    }
}