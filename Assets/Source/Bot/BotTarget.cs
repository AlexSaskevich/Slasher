using System;
using Source.GameLogic;
using Source.Player;
using UnityEngine;

namespace Source.Bot
{
    public sealed class BotTarget : MonoBehaviour
    {
        [field: SerializeField] public int TargetsCount { get; private set; }
        
        public Target[] Targets { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        
        public Target CurrentTarget { get; set; }

        public void Init(PlayerMovement playerMovement, Target[] targets)
        {
            if (targets.Length != TargetsCount)
                throw new ArgumentNullException();
            
            PlayerMovement = playerMovement;
            Targets = new Target[TargetsCount];
            
            for (var i = 0; i < Targets.Length; i++)
                Targets[i] = targets[i];
        }
    }
}