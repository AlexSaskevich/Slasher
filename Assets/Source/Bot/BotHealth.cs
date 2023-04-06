using Source.GameLogic;

namespace Source.Bot
{
    public class BotHealth : Health
    {
        protected override void Die()
        {
            gameObject.SetActive(false);
        }
    }
}