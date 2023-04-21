namespace Source.UI.Views
{
    public sealed class ZombieCurrentScoreView : ZombieScoreView
    {
        private void OnEnable()
        {
            ZombieScoreListener.ZombieScore.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            ZombieScoreListener.ZombieScore.ScoreChanged -= OnScoreChanged;
        }
        
        private void OnScoreChanged(int score)
        {
            Score.text = score.ToString();
        }
    }
}