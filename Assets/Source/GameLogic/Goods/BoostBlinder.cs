using Source.Enums;
using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Goods
{
    public sealed class BoostBlinder : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private int _increasingValue;

        private IUpgradeable _upgradeable;
        private PlayerWallet _playerWallet;

        [field: SerializeField] public GoodStatus GoodStatus { get; private set; }

        public Boost Boost { get; private set; }

        public void Init(PlayerWallet playerWallet, IUpgradeable upgradeable)
        {
            _playerWallet = playerWallet;
            _upgradeable = upgradeable;
            
            Boost = new Boost(GameProgressSaver.GetGoodLevel(GoodStatus), _increasingValue, _upgradeable, _price,
                _playerWallet, GoodStatus);
        }
    }
}