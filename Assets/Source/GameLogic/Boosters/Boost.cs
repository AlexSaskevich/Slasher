using Source.Enums;
using Source.Interfaces;
using Source.Player;

namespace Source.GameLogic.Boosters
{
    public sealed class Boost
    {
        private const int MaxLevel = 5;

        private readonly GoodStatus _goodStatus;
        private readonly PlayerCharacterName _playerCharacterName;
        private readonly int _startIncreasedValue;

        public Boost(int level, int increasedValue, IUpgradeable upgradeable, int price, PlayerWallet playerWallet,
            GoodStatus goodStatus, PlayerCharacterName playerCharacterName)
        {
            Level = level;
            IncreasedValue = increasedValue;
            _startIncreasedValue = increasedValue;
            UIIncreasedValue = _startIncreasedValue + _startIncreasedValue * Level;
            Upgradeable = upgradeable;
            Price = price;
            Wallet = playerWallet;
            _goodStatus = goodStatus;
            _playerCharacterName = playerCharacterName;
        }
        
        public PlayerWallet Wallet { get; }
        public int Price { get; }
        public int IncreasedValue { get; private set; }
        public int UIIncreasedValue { get; private set; }
        public IUpgradeable Upgradeable { get; }
        public int Level { get; private set; }
        
        public void TryBuy()
        {
            if (Wallet.CurrentMoney < Price)
                return;
            
            Wallet.TrySpendMoney(Price);
            
            IncreasedValue = _startIncreasedValue + _startIncreasedValue * Level;
            
            UIIncreasedValue = IncreasedValue + _startIncreasedValue;
            
            Upgradeable.TryUpgrade(IncreasedValue);
                
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