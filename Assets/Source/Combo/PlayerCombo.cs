using Source.Player;
using System;
using UnityEngine;

namespace Source.Combo
{
    [RequireComponent(typeof(Animator), typeof(PlayerHealth), typeof(Rigidbody))]
    public sealed class PlayerCombo : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private readonly MoveState _moveState = new();
        private PlayerHealth _playerHealth;
        private PlayerInput _playerInput;

        public event Action StateChanged;

        [field: SerializeField] public float AgilityPerHit { get; private set; }
        public State CurrentState { get; private set; }
        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public PlayerAgility PlayerAgility { get; private set; }

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            PlayerAgility = GetComponent<PlayerAgility>();
            _playerInput = GetComponent<PlayerInput>();
            CurrentState = _moveState;
            CurrentState.Enter(this);
        }

        private void OnEnable()
        {
            _playerHealth.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _playerHealth.HealthChanged -= OnHealthChanged;
        }

        private void Update()
        {
            CurrentState.Update(this, _playerInput);
        }

        private void OnHealthChanged()
        {
            if (_playerHealth.CurrentHealth <= 0)
            {
                SwitchState(new DeathState());
                return;
            }

            if (CurrentState is FinishState)
                return;

            CurrentState.Exit(this);
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
            PlayerAgility.DecreaseAgility(PlayerAgility.MaxAgility);
        }

        public void StopKill()
        {
            _weapon.StopDetectCollisions();
            _weapon.ApplyDamage(_weapon.MaxDamage);
        }
    }
}