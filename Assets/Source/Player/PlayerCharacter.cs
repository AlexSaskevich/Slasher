using Source.Enums;
using Source.GameLogic;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerCharacter : MonoBehaviour
    {
        [field: SerializeField] public PlayerCharacterName PlayerCharacterName { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

        public bool IsBought { get; private set; }

        private void Awake()
        {
            IsBought = GameProgressSaver.GetCharacterBoughtStatus(PlayerCharacterName);
        }

        public void Buy()
        {
            IsBought = true;
        }
    }
}