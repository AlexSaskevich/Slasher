namespace Source.UI.Views.ScoreViews
{
    public abstract class CurrentScoreView : ScoreView
    {
        private const string StartScore = "0";
        
        private void OnEnable()
        {
            ScoreText.text = StartScore;
        }

        private void OnDisable()
        {
            Score.ScoreChanged -= OnScoreChanged;
        }

        protected override void Init()
        {
            Score.ScoreChanged += OnScoreChanged;
        }
        
        private void OnScoreChanged(int score)
        {
            ScoreText.text = score.ToString();
        }
    }
}