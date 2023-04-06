using Source.Bot;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Combo
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private int _baseDamage;

        private readonly List<BotHealth> _damagedBots = new();
        private BoxCollider _boxCollider;

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

            ApplyDamage();

            _damagedBots.Clear();
        }

        private void ApplyDamage()
        {
            foreach (var botHealth in _damagedBots)
                botHealth.TryTakeDamage(_baseDamage);
        }
    }
}