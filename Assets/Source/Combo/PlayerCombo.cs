using Source.InputSource;
using Source.Interfaces;
using Source.Player;
using System;
using UnityEngine;

namespace Source.Combo
{
    [RequireComponent(typeof(Animator), typeof(PlayerHealth))]
    public sealed class PlayerCombo : MonoBehaviour
    {
        [SerializeField] private PlayerWeapon _weapon;

        private readonly MoveState _moveState = new();
        private PlayerHealth _playerHealth;
        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;

        public event Action StateChanged;

        [field: SerializeField] public float AgilityPerHit { get; private set; }
        public State CurrentState { get; private set; }
        public Animator Animator { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public PlayerAgility PlayerAgility { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }

        private void Awake()
        {
            _inputSwitcher = GetComponent<InputSwitcher>();
            _playerHealth = GetComponent<PlayerHealth>();
            Animator = GetComponent<Animator>();
            CharacterController = GetComponent<CharacterController>();
            PlayerAgility = GetComponent<PlayerAgility>();
            PlayerMovement = GetComponent<PlayerMovement>();
            CurrentState = _moveState;
            CurrentState.Enter(this);
        }

        private void Start()
        {
            _inputSource = _inputSwitcher.InputSource;
        }

        private void OnEnable()
        {
            _playerHealth.HealthChanged += OnHealthChanged;
            _playerHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _playerHealth.HealthChanged -= OnHealthChanged;
            _playerHealth.Died -= OnDied;
        }

        private void Update()
        {
            CurrentState.Update(this, _inputSource);
        }

        private void OnHealthChanged()
        {
            if (CurrentState is ComboState && _playerHealth.IsBuffed == false)
                CurrentState.Exit(this);
        }

        private void OnDied()
        {
            SwitchState(new DeathState());
        }

        public void SwitchState(State newState)
        {
            if (newState == null)
                return;

            CurrentState = newState;
            newState.Enter(this);
            StateChanged?.Invoke();
        }

        public void StartDealingDamage()
        {
            _weapon.StartDetectCollisions();
            PlayerAgility.DecreaseAgility(AgilityPerHit);
        }

        public void StopDealingDamage()
        {
            _weapon.StopDetectCollisions();
            _weapon.ApplyDamage(_weapon.FinalDamage);
        }

        public void StartKill()
        {
            _weapon.StartDetectCollisions();
            PlayerAgility.DecreaseAgility(PlayerAgility.MaxValue);
        }

        public void StopKill()
        {
            _weapon.StopDetectCollisions();
            _weapon.ApplyDamage(_weapon.MaxDamage);
        }
    }
}