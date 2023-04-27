using System;
using Source.Enums;
using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic.Goods
{
    public sealed class Good
    {
        private const int MaxLevel = 5;
        
        private readonly IUpgradeable _upgradeable;
        private readonly PlayerWallet _playerWallet;
        private readonly GoodStatus _goodStatus;
        private readonly int _increasedValue;
        private readonly int _price;

        public Good(int level, int increasedValue, IUpgradeable upgradeable, int price, PlayerWallet playerWallet, GoodStatus goodStatus)
        {
            Level = level;
            _increasedValue = increasedValue;
            _upgradeable = upgradeable;
            _price = price;
            _playerWallet = playerWallet;
            _goodStatus = goodStatus;
        }
        
        public int Level { get; private set; }
        
        public void TryBuy()
        {
            if (_playerWallet.CurrentMoney >= _price)
            {
                _upgradeable.TryUpgrade(_increasedValue + _increasedValue * Level);
                _playerWallet.TrySpendMoney(_price);
                
                Level++;
                GameProgressSaver.SetGoodLevel(_goodStatus, Level);

                var value = _upgradeable.GetUpgradedCharacteristic();

                GameProgressSaver.SetPlayerCharacteristic(_upgradeable.CharacteristicStatus, value);              
            }
            else
            {
                Debug.Log("No money");
            }
        }

        public bool IsMaxLevel()
        {
            return Level >= MaxLevel;
        }
    }
}