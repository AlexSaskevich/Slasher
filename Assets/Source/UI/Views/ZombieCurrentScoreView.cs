namespace Source.UI.Views
{
    public sealed class ZombieCurrentScoreView : ZombieScoreView
    {
        private void OnEnable()
        {
            ZombieScore.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            ZombieScore.ScoreChanged -= OnScoreChanged;
        }
        
        private void OnScoreChanged(int score)
        {
            Score.text = score.ToString();
        }
    }
}