using System;
using Source.GameLogic;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public sealed class PlayerWallet : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        
        public event Action MoneyChanged;

        public int CurrentMoney { get; private set; }

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void OnEnable()
        {
            _playerHealth.Died += OnDied;
            
            CurrentMoney = GameProgressSaver.GetMoney();
        }
        
        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        public void TryTakeMoney(int money)
        {
            if (money <= 0)
                return;

            CurrentMoney += money;

            GameProgressSaver.SetMoney(CurrentMoney);
            MoneyChanged?.Invoke();
        }

        public void TrySpendMoney(int price)
        {
            if (price <= 0)
                return;
            
            CurrentMoney -= price;
            
            GameProgressSaver.SetMoney(CurrentMoney);
            MoneyChanged?.Invoke();
        }

        private void OnDied()
        {
            GameProgressSaver.SetMoney(CurrentMoney);
        }
    }
}