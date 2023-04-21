using Source.GameLogic;

namespace Source.Bot
{
    public sealed class BotZombieHealth : BotHealth
    {
        private ZombieScoreListener _zombieScoreListener;
        
        public void Init(ZombieScoreListener zombieScoreListener)
        {
            _zombieScoreListener = zombieScoreListener;
        }
        
        protected override void Die()
        {
            BotTarget.ClearTargets();

            var newScore = _zombieScoreListener.ZombieScore.Score + 1;
            _zombieScoreListener.ZombieScore.SetScore(newScore);
        }
    }
}