using System;
using Source.GameLogic;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(BotTarget))]
    public class BotHealth : Health
    {
        private BotTarget _botTarget;

        private void Awake()
        {
            _botTarget = GetComponent<BotTarget>();
        }

        protected override void Die()
        {
            _botTarget.ClearTargets();
            
            gameObject.SetActive(false);
        }
    }
}