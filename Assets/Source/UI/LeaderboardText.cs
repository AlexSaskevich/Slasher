using Source.Enums;
using UnityEngine;

namespace Source.UI
{
    public sealed class LeaderboardText : MonoBehaviour
    {
        [field: SerializeField] public LeaderboardName LeaderboardName { get; private set; }
    }
}