namespace Source.UI.Views
{
    public sealed class ZombieCurrentScoreView : ZombieScoreView
    {
        private const string StartScore = "0";
        
        private void OnEnable()
        {
            Score.text = StartScore;
        }

        private void OnDisable()
        {
            ZombieScore.ScoreChanged -= OnScoreChanged;
        }

        protected override void Init()
        {
            ZombieScore.ScoreChanged += OnScoreChanged;
        }
        
        private void OnScoreChanged(int score)
        {
            Score.text = score.ToString();
        }
    }
}