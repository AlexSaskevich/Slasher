using Source.Enums;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class Target : MonoBehaviour
    {
        [field: SerializeField] public BotStatus Status { get; private set; }

        public bool IsAvailable { get; private set; } = true;

        public void SetAvailableStatus(bool state)
        {
            IsAvailable = state;
        }
    }
}