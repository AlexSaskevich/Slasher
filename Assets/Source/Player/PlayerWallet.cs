using System;
using Source.GameLogic;

namespace Source.Player
{
    public sealed class PlayerWallet
    {
        public int CurrentMoney { get; private set; }

        public event Action MoneyChanged;

        public PlayerWallet(int currentMoney)
        {
            CurrentMoney = currentMoney;
        }
        
        public void TryTakeMoney(int money)
        {
            if (money <= 0)
                return;

            CurrentMoney += money;

            MoneyChanged?.Invoke();
            GameProgressSaver.SetMoney(CurrentMoney);
        }

        public void TrySpendMoney(int price)
        {
            if (price <= 0)
                return;
            
            CurrentMoney -= price;
            
            MoneyChanged?.Invoke();
            GameProgressSaver.SetMoney(CurrentMoney);
        }
    }
}