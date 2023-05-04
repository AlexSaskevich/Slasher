using System;
using Source.Bot;
using UnityEngine;

namespace Source.GameLogic.BotsSpawners
{
    [Serializable]
    public sealed class Wave
    {
        [SerializeField] private BotMovement[] _botMovements;
        
        [field: SerializeField] public int BotsCount { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }

        public BotMovement TryGetBotMovement(int index)
        {
            if (index < 0 || index >= _botMovements.Length)
                return null;
            
            return _botMovements[index];
        }

        public int GetBotsMovementsLength()
        {
            return _botMovements.Length;
        }
    }
}