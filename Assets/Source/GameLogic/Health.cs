using UnityEngine;

namespace Source.GameLogic
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        
        private float _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void TryTakeDamage(float damage)
        {
            if (damage <= 0)
                return;

            _health -= damage;

            if (_health <= 0)
                Die();
        }

        public void ResetHealth()
        {
            _health = _maxHealth;
        }

        protected abstract void Die();
    }
}