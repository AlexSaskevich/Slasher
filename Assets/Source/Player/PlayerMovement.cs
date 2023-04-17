using Source.Combo;
using Source.Interfaces;
using System;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerCombo))]
    public sealed class PlayerMovement : MonoBehaviour, IMoveable, IBuffable
    {
        private Rigidbody _rigidbody;
        private PlayerCombo _playerCombo;

        [field: SerializeField] public float DefaultSpeed { get; private set; }
        public float FinalSpeed { get; private set; }
        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerCombo = GetComponent<PlayerCombo>();
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
            _rigidbody.velocity = Vector3.zero;
        }

        public void Move(float directionX, float directionZ)
        {
            if (_rigidbody.velocity.magnitude >= FinalSpeed)
                return;

            var direction = new Vector3(directionX, _rigidbody.velocity.y, directionZ).normalized;
            _rigidbody.velocity = new Vector3(direction.x * FinalSpeed, direction.y, direction.z * FinalSpeed);
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