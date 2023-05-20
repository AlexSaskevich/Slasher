using Source.GameLogic.Scores;
using Source.GameLogic.Timers;
using UnityEngine;

namespace Source.Bot
{
    public sealed class BotHumanHealth : BotHealth
    {
        [SerializeField] private int _increasedSeconds;
        
        private FirstGameModeTimer _firstGameModeTimer;
        private TimeModeScore _timeModeScore;
        private bool _isGameModeTimeMode;

        public void Init(TimeModeScore timeModeScore, bool isGameModeTimeMode, FirstGameModeTimer firstGameModeTimer)
        {
            _firstGameModeTimer = firstGameModeTimer;
            _timeModeScore = timeModeScore;
            _isGameModeTimeMode = isGameModeTimeMode;
        }
        
        protected override void Die()
        {
            BotTarget.ClearTargets();

            if (_isGameModeTimeMode)
            {
                _firstGameModeTimer.TryIncreaseSeconds(_increasedSeconds);
                _timeModeScore.SetScore(_timeModeScore.CurrentScore + 1);
            }
            
            gameObject.SetActive(false);
        }
    }
}