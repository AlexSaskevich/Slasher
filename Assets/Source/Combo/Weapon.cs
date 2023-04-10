using Source.Bot;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Combo
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Weapon : MonoBehaviour
    {
        private readonly List<BotHealth> _damagedBots = new();
        private BoxCollider _boxCollider;

        [field: SerializeField] public int DefaultDamage { get; private set; }
        [field: SerializeField] public int MaxDamage { get; private set; }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BotHealth botHealth) == false)
                return;

            if (_damagedBots.Contains(botHealth))
                return;

            _damagedBots.Add(botHealth);
        }

        public void StartDetectCollisions()
        {
            _boxCollider.enabled = true;
        }

        public void StopDetectCollisions()
        {
            _boxCollider.enabled = false;
        }

        public void ApplyDamage(float damage)
        {
            foreach (var botHealth in _damagedBots)
                botHealth.TryTakeDamage(damage);

            _damagedBots.Clear();
        }
    }
}