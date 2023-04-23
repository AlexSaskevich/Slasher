using Source.GameLogic;

namespace Source.Bot
{
    public sealed class BotZombieHealth : BotHealth
    {
        private ZombieScore _zombieScore;
        
        public void Init(ZombieScore zombieScoreListener)
        {
            _zombieScore = zombieScoreListener;
        }
        
        protected override void Die()
        {
            BotTarget.ClearTargets();
            _zombieScore.SetScore(_zombieScore.Score + 1);
            
            gameObject.SetActive(false);
        }
    }
}