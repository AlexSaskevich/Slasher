using System;

namespace Source.Player
{
    public sealed class PlayerWallet
    {
        private int _currentMoney;

        public event Action<int> MoneyChanged;

        public void TryTakeMoney(int money)
        {
            if (money <= 0)
                return;

            _currentMoney += money;

            MoneyChanged?.Invoke(_currentMoney);
        }

        public void TrySpendMoney(int price)
        {
            if (price <= 0)
                return;

            _currentMoney -= price;
        }
    }
}