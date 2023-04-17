using System;
using Source.GameLogic;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(BotTarget))]
    public class BotHealth : Health
    {
        private BotTarget _botTarget;

        public event Action Died;
        
        private void Awake()
        {
            _botTarget = GetComponent<BotTarget>();
        }

        protected override void Die()
        {
            Died?.Invoke();
            _botTarget.ClearTargets();
            
            gameObject.SetActive(false);
        }
    }
}