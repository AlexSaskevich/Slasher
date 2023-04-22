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
            CurrentMoney = GameProgressSaver.GetMoney();
        }

        private void OnEnable()
        {
            _playerHealth.Died += OnDied;
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

        private void OnDied()
        {
            GameProgressSaver.SetMoney(CurrentMoney);
        }
    }
}