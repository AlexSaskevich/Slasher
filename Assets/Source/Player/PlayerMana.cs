using System;
using Source.Enums;
using Source.GameLogic;
using Source.Interfaces;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerCharacter))]
    public sealed class PlayerMana : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private float _increasingMana;

        private PlayerCharacter _playerCharacter;

        public event Action ManaChanged;

        [field: SerializeField] public CharacteristicStatus CharacteristicStatus { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }
        
        public float CurrentValue { get; private set; }

        private void Awake()
        {
            _playerCharacter = GetComponent<PlayerCharacter>();
            var maxMana =
                GameProgressSaver.GetPlayerCharacteristic(_playerCharacter.PlayerCharacterName, CharacteristicStatus);

            if (maxMana > 0)
                MaxValue = maxMana;
            
            CurrentValue = MaxValue;
        }

        private void Update()
        {
            if (CurrentValue < MaxValue)
                IncreaseMana();
        }

        public void DecreaseMana(float value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - value, 0, MaxValue);
            ManaChanged?.Invoke();
        }

        public void TryUpgrade(float value)
        {
            if (value <= 0)
                return;

            MaxValue += value;
            CurrentValue = MaxValue;
        }

        public float GetUpgradedCharacteristic()
        {
            return MaxValue;
        }
        
        private void IncreaseMana()
        {
            CurrentValue = Mathf.Clamp(CurrentValue + _increasingMana * Time.deltaTime, 0, MaxValue);
            ManaChanged?.Invoke();
        }
    }
}