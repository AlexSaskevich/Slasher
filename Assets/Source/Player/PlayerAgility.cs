using Source.Combo;
using System;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerAgility : MonoBehaviour
    {
        [SerializeField] private float _increasingAgility;
        [SerializeField] private float _decreasingAgility;

        private PlayerCombo _playerCombo;

        public event Action AgilityChanged;

        [field: SerializeField] public float MaxAgility { get; private set; }
        public float Agility { get; private set; }

        private void Awake()
        {
            Agility = MaxAgility;
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
            if (_playerCombo.CurrentState is MoveState && Agility < MaxAgility)
                IncreaseAgility();
        }

        private void OnAttacked()
        {
            if (_playerCombo.CurrentState is FinishState)
            {
                DecreaseAgility(MaxAgility);
                return;
            }

            DecreaseAgility(_decreasingAgility);
        }

        private void IncreaseAgility()
        {
            Agility = Mathf.Clamp(Agility + _increasingAgility, 0, MaxAgility);
            AgilityChanged?.Invoke();
        }

        private void DecreaseAgility(float value)
        {
            Agility = Mathf.Clamp(Agility - value, 0, MaxAgility);
            AgilityChanged?.Invoke();
            Debug.Log(Agility);
        }
    }
}