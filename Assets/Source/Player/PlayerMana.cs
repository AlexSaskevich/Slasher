using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerMana : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private float _increasingMana;

        public event Action ManaChanged;

        [field: SerializeField] public float MaxMana { get; private set; }
        public float CurrentMana { get; private set; }

        private void Awake()
        {
            CurrentMana = MaxMana;
        }

        private void Update()
        {
            if (CurrentMana < MaxMana)
                IncreaseMana();
        }

        public void DecreaseMana(float value)
        {
            CurrentMana = Mathf.Clamp(CurrentMana - value, 0, MaxMana);
            ManaChanged?.Invoke();
        }

        private void IncreaseMana()
        {
            CurrentMana = Mathf.Clamp(CurrentMana + _increasingMana * Time.deltaTime, 0, MaxMana);
            ManaChanged?.Invoke();
        }

        public void TryUpgrade(float value)
        {
            if (value <= 0)
                return;

            MaxMana += value;
            CurrentMana = MaxMana;
        }
    }
}