using Source.Combo;
using Source.Interfaces;
using System;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerCombo), typeof(CharacterController))]
    public sealed class PlayerMovement : MonoBehaviour, IMoveable, IBuffable
    {
        private PlayerCombo _playerCombo;
        private CharacterController _characterController;

        [field: SerializeField] public float DefaultSpeed { get; private set; }
        public float FinalSpeed { get; private set; }
        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _playerCombo = GetComponent<PlayerCombo>();
            _characterController = GetComponent<CharacterController>();
            FinalSpeed = DefaultSpeed;
        }

        private void OnEnable()
        {
            _playerCombo.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _playerCombo.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (IsBuffed)
                return;

            if (_playerCombo.CurrentState is MoveState || _playerCombo.CurrentState is EntryState)
            {
                FinalSpeed = DefaultSpeed;
                return;
            }

            FinalSpeed = 0;
        }

        public void Move(float directionX, float directionZ)
        {
            var direction = new Vector3(directionX, 0, directionZ);
            direction = Vector3.Normalize(direction);

            _characterController.Move(direction * FinalSpeed * Time.deltaTime);
        }

        public void AddModifier(float modifier)
        {
            if (modifier == 0)
                throw new ArgumentException();

            IsBuffed = true;
            FinalSpeed *= modifier;
        }

        public void RemoveModifier(float modifier)
        {
            if (modifier == 0)
                throw new ArgumentException();

            IsBuffed = false;
            FinalSpeed /= modifier;
        }
    }
}