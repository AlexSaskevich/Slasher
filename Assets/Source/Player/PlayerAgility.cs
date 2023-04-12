using Source.Combo;
using System;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerAgility : MonoBehaviour
    {
        [SerializeField] private float _increasingAgility;

        private PlayerCombo _playerCombo;

        public event Action AgilityChanged;

        [field: SerializeField] public float MaxAgility { get; private set; }
        [field: SerializeField] public float AgilityPerHit { get; private set; }
        public float CurrentAgility { get; private set; }

        private void Awake()
        {
            CurrentAgility = MaxAgility;
            _playerCombo = GetComponent<PlayerCombo>();
        }

        private void OnEnable()
        {
            _playerCombo.Attacked += OnAttacked;
        }

        private void OnDisable()
        {
            _playerCombo.Attacked -= OnAttacked;
        }

        private void Update()
        {
            if (_playerCombo.CurrentState is MoveState && CurrentAgility < MaxAgility)
                IncreaseAgility();
        }

        public void DecreaseAgility(float value)
        {
            CurrentAgility = Mathf.Clamp(CurrentAgility - value, 0, MaxAgility);
            AgilityChanged?.Invoke();
        }

        private void OnAttacked()
        {
            if (_playerCombo.CurrentState is FinishState)
            {
                DecreaseAgility(MaxAgility);
                return;
            }

            DecreaseAgility(AgilityPerHit);
        }

        private void IncreaseAgility()
        {
            CurrentAgility = Mathf.Clamp(CurrentAgility + _increasingAgility, 0, MaxAgility);
            AgilityChanged?.Invoke();
        }
    }
}