using Source.Bot;
using Source.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
    public sealed class PlayerWeapon : MonoBehaviour, IBuffable
    {
        private readonly List<BotHealth> _damagedBots = new();
        private BoxCollider _boxCollider;

        [field: SerializeField] public float DefaultDamage { get; private set; }
        [field: SerializeField] public float MaxDamage { get; private set; }
        public float FinalDamage { get; private set; }
        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
            FinalDamage = DefaultDamage;
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

        public void AddModifier(float modifier)
        {
            if (modifier == 0)
                throw new ArgumentException();

            IsBuffed = true;
            FinalDamage *= modifier;
        }

        public void RemoveModifier(float modifier)
        {
            if (modifier == 0)
                throw new ArgumentException();

            IsBuffed = false;
            FinalDamage /= modifier;
        }
    }
}