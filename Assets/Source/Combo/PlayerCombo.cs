using UnityEngine;

namespace Source.Combo
{
    public sealed class PlayerCombo : MonoBehaviour
    {
        [SerializeField] private CollisionDetection _collisionDetection;

        private readonly IdleState _idleState = new IdleState();
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
            _collisionDetection.StartDetect();
        }

        public void StopDetectCollisions()
        {
            _collisionDetection.StopDetect();
        }
    }
}