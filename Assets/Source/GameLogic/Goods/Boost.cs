using Source.Enums;
using Source.Interfaces;
using Source.Player;

namespace Source.GameLogic.Goods
{
    public sealed class Boost
    {
        private const int MaxLevel = 5;

        private readonly PlayerWallet _playerWallet;
        private readonly GoodStatus _goodStatus;
        private readonly int _price;

        public Boost(int level, int increasedValue, IUpgradeable upgradeable, int price, PlayerWallet playerWallet,
            GoodStatus goodStatus)
        {
            Level = level;
            IncreasedValue = increasedValue;
            Upgradeable = upgradeable;
            _price = price;
            _playerWallet = playerWallet;
            _goodStatus = goodStatus;
        }
        
        public IUpgradeable Upgradeable { get; }
        public int IncreasedValue { get; }
        public int Level { get; private set; }
        
        public void TryBuy()
        {
            if (_playerWallet.CurrentMoney < _price)
                return;
            
            Upgradeable.TryUpgrade(IncreasedValue + IncreasedValue * Level);
            _playerWallet.TrySpendMoney(_price);
                
            Level++;
            GameProgressSaver.SetGoodLevel(_goodStatus, Level);

            var value = Upgradeable.GetUpgradedCharacteristic();

            GameProgressSaver.SetPlayerCharacteristic(Upgradeable.CharacteristicStatus, value);
        }

        public bool IsMaxLevel()
        {
            return Level >= MaxLevel;
        }
    }
}