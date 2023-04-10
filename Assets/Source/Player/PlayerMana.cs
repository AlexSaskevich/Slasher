using System;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerMana : MonoBehaviour
    {
        [SerializeField] private float _increasingMana;
        [SerializeField] private float _decreasingMana;

        public event Action ManaChanged;

        [field: SerializeField] public float MaxMana { get; private set; }
        public float Mana { get; private set; }

        private void Start()
        {
            Mana = MaxMana;
        }

        private void Update()
        {
            if (Mana >= MaxMana)
                return;

            Mana += _increasingMana;
        }

        public void TryTakeMana(float value)
        {
            if (value <= 0)
                return;

            Mana -= _decreasingMana;

            ManaChanged?.Invoke();
        }
    }
}