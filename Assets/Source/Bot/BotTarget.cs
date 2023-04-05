using Source.GameLogic;
using Source.Player;
using UnityEngine;

namespace Source.Bot
{
    public sealed class BotTarget : MonoBehaviour
    {
        [field: SerializeField] public Target[] Targets { get; private set; }
        [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
        
        public Target CurrentTarget { get; set; }
    }
}