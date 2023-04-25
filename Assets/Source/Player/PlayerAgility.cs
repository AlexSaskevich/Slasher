using Source.Combo;
using Source.Interfaces;
using System;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerAgility : MonoBehaviour, IBuffable
    {
        [SerializeField] private float _increasingAgility;

        private PlayerCombo _playerCombo;

        public event Action AgilityChanged;

        [field: SerializeField] public float MaxAgility { get; private set; }
        public float CurrentAgility { get; private set; }

        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            CurrentAgility = MaxAgility;
            _playerCombo = GetComponent<PlayerCombo>();
        }

        private void Update()
        {
            if (_playerCombo.CurrentState is MoveState && CurrentAgility < MaxAgility)
                IncreaseAgility();
        }

        public void DecreaseAgility(float value)
        {
            if (IsBuffed)
                value = 0;

            CurrentAgility = Mathf.Clamp(CurrentAgility - value, 0, MaxAgility);
            AgilityChanged?.Invoke();
        }

        private void IncreaseAgility()
        {
            CurrentAgility = Mathf.Clamp(CurrentAgility + _increasingAgility * Time.deltaTime, 0, MaxAgility);
            AgilityChanged?.Invoke();
        }

        public void AddModifier(float modifier)
        {
            if (modifier <= 0)
                throw new ArgumentException();

            IsBuffed = true;
        }

        public void RemoveModifier(float modifier)
        {
            if (modifier <= 0)
                throw new ArgumentException();

            IsBuffed = false;
        }
    }
}