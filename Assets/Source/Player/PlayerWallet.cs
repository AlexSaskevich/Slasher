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
    }
}