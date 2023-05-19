using Source.Enums;
using UnityEngine;

namespace Source.UI
{
    public sealed class PlayerDescription : MonoBehaviour
    {
        [field: SerializeField] public PlayerCharacterName PlayerCharacterName { get; private set; }
    }
}