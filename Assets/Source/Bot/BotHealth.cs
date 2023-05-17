using Source.GameLogic;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(BotTarget))]
    public abstract class BotHealth : Health
    {
        protected BotTarget BotTarget { get; private set; }

        private void Awake()
        {
            BotTarget = GetComponent<BotTarget>();
        }
    }
}