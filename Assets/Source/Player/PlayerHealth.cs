using Source.GameLogic;

namespace Source.Player
{
    public sealed class PlayerHealth : Health
    {
        protected override void Die()
        {
            gameObject.SetActive(false);
        }
    }
}