using Source.GameLogic.Scores;

namespace Source.Bot
{
    public sealed class BotHumanHealth : BotHealth
    {
        private TimeModeScore _timeModeScore;
        private bool _isGameModeTimeMode;

        public void Init(TimeModeScore timeModeScore, bool isGameModeTimeMode)
        {
            _timeModeScore = timeModeScore;
            _isGameModeTimeMode = isGameModeTimeMode;
        }
        
        protected override void Die()
        {
            BotTarget.ClearTargets();

            if (_isGameModeTimeMode)
                _timeModeScore.SetScore(_timeModeScore.CurrentScore + 1);
            
            gameObject.SetActive(false);
        }
    }
}