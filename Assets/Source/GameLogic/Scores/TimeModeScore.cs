using Source.Player;
using Source.Yandex;
using UnityEngine;

namespace Source.GameLogic.Scores
{
    [RequireComponent(typeof(PlayerHealth))]
    public sealed class TimeModeScore : Score
    {
        protected override void Awake()
        {
            TrySetHighestScore(GameProgressSaver.GetTimeModeScore());
            base.Awake();
        }

        public void TrySetScore()
        {
            if (CurrentScore <= HighestScore)
                return;
            
            GameProgressSaver.SetTimeModeScore(CurrentScore);
            Leaderboard.AddPlayer(CurrentScore, LeaderboardName.ToString());
        }
    }
}