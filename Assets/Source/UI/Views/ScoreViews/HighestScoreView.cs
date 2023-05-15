namespace Source.UI.Views.ScoreViews
{
    public abstract class HighestScoreView : ScoreView
    {
        protected override void Init()
        {
            ScoreText.text = Score.HighestScore.ToString();
        }
    }
}