using UnityEngine;
using UnityEngine.Events;

namespace Source.GameLogic
{
    public abstract class Health : MonoBehaviour
    {
        [field: SerializeField] public float MaxHealth;

        public event UnityAction HealthChanged;

        public float CurrentHealth { get; private set; }

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void TryTakeDamage(float damage)
        {
            if (damage <= 0)
                return;

            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
                Die();

            HealthChanged?.Invoke();
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
        }

        protected abstract void Die();
    }
}