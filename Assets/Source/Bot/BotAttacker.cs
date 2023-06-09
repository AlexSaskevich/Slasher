﻿using Source.Player;
using UnityEngine;

namespace Source.Bot
{
    public abstract class BotAttacker : MonoBehaviour
    {
        [SerializeField] private float _attackingRadius;
        [SerializeField] private BotAttackingTrigger _attackingTrigger;

        [field: SerializeField] public float Delay { get; private set; }
        
        public PlayerHealth PlayerHealth { get; private set; }
        public bool IsPlayerDetected { get; private set; }
        
        private void OnEnable()
        {
            IsPlayerDetected = false;
            
            _attackingTrigger.PlayerDetected += OnPlayerDetected;
            _attackingTrigger.PlayerLeft += OnPlayerLeft;
        }

        private void OnDisable()
        {
            _attackingTrigger.PlayerDetected -= OnPlayerDetected;
            _attackingTrigger.PlayerLeft -= OnPlayerLeft;
        }

        private void Start()
        {
            _attackingTrigger.Init(_attackingRadius);
        }

        public void Init(PlayerHealth playerHealth)
        {
            PlayerHealth = playerHealth;
        }

        private void OnPlayerDetected()
        {
            IsPlayerDetected = true;
        }

        private void OnPlayerLeft()
        {
            IsPlayerDetected = false;
        }

        public abstract void Attack();
    }
}
