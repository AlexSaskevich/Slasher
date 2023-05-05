using Source.Enums;
using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Boosters
{
    public sealed class BoostBlinder : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private int _increasingValue;

        [field: SerializeField] public GoodStatus GoodStatus { get; private set; }

        public Boost Boost { get; private set; }

        public void Init(PlayerWallet playerWallet, IUpgradeable upgradeable, PlayerCharacterName playerCharacterName)
        {
            Boost = new Boost(GameProgressSaver.GetBoosterLevel(playerCharacterName, GoodStatus), _increasingValue,
                upgradeable, _price, playerWallet, GoodStatus, playerCharacterName);
        }
    }
}