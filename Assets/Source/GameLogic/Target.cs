using Source.Enums;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class Target : MonoBehaviour
    {
        [field: SerializeField] public BotStatus Status { get; private set; }

        public bool IsAvailable { get; private set; } = true;

        public void MakeUnavailable()
        {
            IsAvailable = false;
        }
    }
}