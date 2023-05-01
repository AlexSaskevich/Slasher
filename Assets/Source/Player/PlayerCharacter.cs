using Source.Enums;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerCharacter : MonoBehaviour
    {
        [field: SerializeField] public PlayerCharacterName PlayerCharacterName { get; private set; }
        [field: SerializeField] public float Price { get; private set; }

        public bool IsBought { get; private set; }

        public void Buy()
        {
            IsBought = true;
        }
    }
}