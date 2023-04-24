﻿using System;
using UnityEngine;

namespace Source.GameLogic
{
    public abstract class Health : MonoBehaviour
    {
        public event Action HealthChanged;
        public event Action Died;

        [field: SerializeField] public float MaxHealth { get; private set; }
        public float CurrentHealth { get; protected set; }

        protected virtual void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public virtual void TryTakeDamage(float damage)
        {
            if (damage <= 0)
                return;

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

            if (CurrentHealth <= 0)
            {
                Died?.Invoke();
                Die();
            }

            HealthChanged?.Invoke();
        }

        protected void TryHeal(float modifier)
        {
            if (modifier <= 0)
                return;

            CurrentHealth = Mathf.Clamp(CurrentHealth + modifier * Time.deltaTime, 0, MaxHealth);
            HealthChanged?.Invoke();
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
        }

        protected abstract void Die();
    }
}