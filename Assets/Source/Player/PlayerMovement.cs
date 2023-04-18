using Source.Combo;
using Source.InputSource;
using Source.Interfaces;
using System;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerCombo), typeof(CharacterController))]
    public sealed class PlayerMovement : MonoBehaviour, IBuffable
    {
        private PlayerCombo _playerCombo;
        private CharacterController _characterController;
        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;

        [field: SerializeField] public float DefaultSpeed { get; private set; }
        public float FinalSpeed { get; private set; }
        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _inputSwitcher = GetComponent<InputSwitcher>();
            //_inputSource = _inputSwitcher.InputSource;
            _playerCombo = GetComponent<PlayerCombo>();
            _characterController = GetComponent<CharacterController>();
            FinalSpeed = DefaultSpeed;
        }

        private void Start()
        {
            _inputSource = _inputSwitcher.InputSource;
        }

        private void OnEnable()
        {
            _playerCombo.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _playerCombo.StateChanged -= OnStateChanged;
        }

        private void Update()
        {
            Move();
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

        private void Move()
        {
            var direction = new Vector3(_inputSource.MovementInput.x, 0, _inputSource.MovementInput.z);
            direction = Vector3.Normalize(direction);

            _characterController.Move(direction * FinalSpeed * Time.deltaTime);
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
    }
}