using Source.GameLogic;
using UnityEngine;

namespace Source.Behavior_Tree
{
    public sealed class BotTarget : MonoBehaviour
    {
        [field: SerializeField] public Target[] Targets { get; private set; }
        public Target CurrentTarget { get; set; }
    }
}