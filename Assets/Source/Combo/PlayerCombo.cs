using UnityEngine;

namespace Source.Combo
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerCombo : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private readonly IdleState _idleState = new();
        private State _currentState;
        private Animator _animator;

        public Animator Animator { get { return _animator; } }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentState = _idleState;
            _currentState.Enter(this);
        }

        private void Update()
        {
            _currentState.Update(this);
        }

        public void SwitchState(State newState)
        {
            if (newState == null)
                return;

            _currentState = newState;
            newState.Enter(this);
        }

        public void StartDetectCollisions()
        {
            _weapon.StartDetectCollisions();
        }

        public void StopDetectCollisions()
        {
            _weapon.StopDetectCollisions();
        }
    }
}