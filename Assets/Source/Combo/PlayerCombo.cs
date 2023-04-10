using Source.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Combo
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerCombo : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private readonly IdleState _idleState = new();
        private Animator _animator;
        private PlayerInput _playerInput;

        public event UnityAction Attacked;
        public event UnityAction StateChanged;

        public State CurrentState { get; private set; }
        public Animator Animator { get { return _animator; } }
        public bool IsAttackButtonClicked { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            CurrentState = _idleState;
            CurrentState.Enter(this);
        }

        private void OnEnable()
        {
            _playerInput.AttackButtonClicked += OnAttackButtonClicked;
        }

        private void OnDisable()
        {
            _playerInput.AttackButtonClicked -= OnAttackButtonClicked;
        }

        private void Update()
        {
            CurrentState.Update(this);
        }

        private void OnAttackButtonClicked(bool value)
        {
            IsAttackButtonClicked = value;
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
            Attacked?.Invoke();
        }

        public void StopDealingDamage()
        {
            _weapon.StopDetectCollisions();
            _weapon.ApplyDamage(_weapon.DefaultDamage);
        }

        public void StartKill()
        {
            _weapon.StartDetectCollisions();
            Attacked?.Invoke();
        }

        public void StopKill()
        {
            _weapon.StopDetectCollisions();
            _weapon.ApplyDamage(_weapon.MaxDamage);
        }
    }
}