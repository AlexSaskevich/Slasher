using Source.Enums;
using Source.Interfaces;
using Source.Player;

namespace Source.GameLogic.Boosters
{
    public sealed class Boost
    {
        private const int MaxLevel = 5;

        private readonly PlayerWallet _playerWallet;
        private readonly GoodStatus _goodStatus;
        private readonly PlayerCharacterName _playerCharacterName;

        public Boost(int level, int increasedValue, IUpgradeable upgradeable, int price, PlayerWallet playerWallet,
            GoodStatus goodStatus, PlayerCharacterName playerCharacterName)
        {
            Level = level;
            IncreasedValue = increasedValue;
            Upgradeable = upgradeable;
            Price = price;
            _playerWallet = playerWallet;
            _goodStatus = goodStatus;
            _playerCharacterName = playerCharacterName;
        }
        
        public int Price { get; }
        public IUpgradeable Upgradeable { get; }
        public int IncreasedValue { get; }
        public int Level { get; private set; }
        
        public void TryBuy()
        {
            if (_playerWallet.CurrentMoney < Price)
                return;
            
            _playerWallet.TrySpendMoney(Price);
            Upgradeable.TryUpgrade(IncreasedValue + IncreasedValue * Level);
                
            Level++;
            GameProgressSaver.SetBoosterLevel(_playerCharacterName, _goodStatus, Level);

            var value = Upgradeable.GetUpgradedCharacteristic();

            GameProgressSaver.SetPlayerCharacteristic(_playerCharacterName, Upgradeable.CharacteristicStatus, value);
        }

        public bool IsMaxLevel()
        {
            return Level >= MaxLevel;
        }
    }
}