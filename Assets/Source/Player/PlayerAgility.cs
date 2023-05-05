using Source.Combo;
using Source.Interfaces;
using System;
using Source.Enums;
using Source.GameLogic;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerCharacter))]
    public sealed class PlayerAgility : MonoBehaviour, IBuffable, IUpgradeable
    {
        [SerializeField] private float _increasingAgility;

        private PlayerCombo _playerCombo;
        private PlayerCharacter _playerCharacter;

        public event Action AgilityChanged;

        [field: SerializeField] public float MaxValue { get; private set; }
        [field: SerializeField] public CharacteristicStatus CharacteristicStatus { get; set; }
        
        public float CurrentAgility { get; private set; }

        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerCombo = GetComponent<PlayerCombo>();
            
            var maxAgility =
                GameProgressSaver.GetPlayerCharacteristic(_playerCharacter.PlayerCharacterName, CharacteristicStatus);

            if (maxAgility > 0)
                MaxValue = maxAgility;
            
            CurrentAgility = MaxValue;
        }

        private void Update()
        {
            if (_playerCombo.CurrentState is MoveState && CurrentAgility < MaxValue)
                IncreaseAgility();
        }

        public void DecreaseAgility(float value)
        {
            if (IsBuffed)
                value = 0;

            CurrentAgility = Mathf.Clamp(CurrentAgility - value, 0, MaxValue);
            AgilityChanged?.Invoke();
        }

        private void IncreaseAgility()
        {
            CurrentAgility = Mathf.Clamp(CurrentAgility + _increasingAgility * Time.deltaTime, 0, MaxValue);
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

        public void TryUpgrade(float value)
        {
            if (value <= 0)
                return;
            
            MaxValue += value;
            CurrentAgility = MaxValue;
        }

        public float GetUpgradedCharacteristic()
        {
            return MaxValue;
        }
    }
}