using Source.GameLogic.Timers;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Scores
{
    [RequireComponent(typeof(PlayerHealth))]
    public sealed class TimeModeScore : Score
    {
        private FirstGameModeBlinder _firstGameModeBlinder;
        
        protected override void Awake()
        {
            TrySetHighestScore(GameProgressSaver.GetTimeModeScore());
            base.Awake();
        }

        private void OnEnable()
        {
            if (_firstGameModeBlinder != null)
                _firstGameModeBlinder.FirstGameModeTimer.Ended += OnEnded;
        }

        private void OnDisable()
        {
            if (_firstGameModeBlinder != null)
                _firstGameModeBlinder.FirstGameModeTimer.Ended -= OnEnded;
        }

        public void Init(FirstGameModeBlinder firstGameModeBlinder)
        {
            _firstGameModeBlinder = firstGameModeBlinder;
        }

        private void OnEnded()
        {
            if (CurrentScore > HighestScore)
                GameProgressSaver.SetTimeModeScore(CurrentScore);
        }
    }
}