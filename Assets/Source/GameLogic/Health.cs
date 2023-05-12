using System;
using UnityEngine;

namespace Source.GameLogic
{
    public abstract class Health : MonoBehaviour
    {
        public event Action HealthChanged;
        public event Action Died;

        [field: SerializeField] public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        protected virtual void Start()
        {
            ResetHealth();
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
        
        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
            HealthChanged?.Invoke();
        }

        protected void TryHeal(float modifier)
        {
            if (modifier <= 0)
                return;

            CurrentHealth = Mathf.Clamp(CurrentHealth + modifier * Time.deltaTime, 0, MaxHealth);
            HealthChanged?.Invoke();
        }

        protected void TryIncreaseMaxHealth(float value)
        {
            if (value <= MaxHealth)
                return;

            MaxHealth = value;
        }

        protected void TrySetMaxHealth(float value)
        {
            if (value <= 0)
                return;

            MaxHealth = value;
        }

        protected abstract void Die();
    }
}