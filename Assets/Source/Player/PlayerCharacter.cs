using Source.Enums;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerCharacter : MonoBehaviour
    {
        [field: SerializeField] public PlayerCharacterName PlayerCharacterName { get; private set; }
    }
}