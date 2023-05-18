using Source.Combo;
using Source.InputSource;
using Source.Interfaces;
using System;
using Source.SoundTurntables;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(StepSoundTurntable))]
    [RequireComponent(typeof(PlayerCombo), typeof(CharacterController))]
    public sealed class PlayerMovement : MonoBehaviour, IBuffable
    {
        private PlayerCombo _playerCombo;
        private CharacterController _characterController;
        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;
        private StepSoundTurntable _stepSoundTurntable;
        private float _buffedSpeed;

        [field: SerializeField] public float DefaultSpeed { get; private set; }
        public float FinalSpeed { get; private set; }
        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _stepSoundTurntable = GetComponent<StepSoundTurntable>();
            _inputSwitcher = GetComponent<InputSwitcher>();
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
            if (modifier <= 0)
                throw new ArgumentException();

            IsBuffed = true;
            FinalSpeed *= modifier;
            _buffedSpeed = FinalSpeed;
        }

        public void RemoveModifier(float modifier)
        {
            if (modifier <= 0)
                throw new ArgumentException();

            IsBuffed = false;
            FinalSpeed /= modifier;
            _buffedSpeed = FinalSpeed;
        }

        public void SetSpeed(float speed)
        {
            FinalSpeed = IsBuffed ? _buffedSpeed : speed;
        }

        private void Move()
        {
            var direction = new Vector3(_inputSource.MovementInput.x, 0, _inputSource.MovementInput.z);
            direction = Vector3.Normalize(direction);
            
            _characterController.Move(direction * (FinalSpeed * Time.deltaTime));
            _stepSoundTurntable.PlayStepAudioClip();
            
            if (_inputSource.MovementInput is { x: 0, z: 0 })
                _stepSoundTurntable.StopStepsSound();
        }

        private void OnStateChanged()
        {
            if (_playerCombo.CurrentState is MoveState or EntryState)
            {
                FinalSpeed = IsBuffed ? _buffedSpeed : DefaultSpeed;
                return;
            }

            FinalSpeed = 0;
        }
    }
}