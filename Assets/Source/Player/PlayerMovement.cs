using Source.Combo;
using Source.Interfaces;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerCombo))]
    public sealed class PlayerMovement : MonoBehaviour, IMoveable
    {
        private Rigidbody _rigidbody;
        private PlayerCombo _playerCombo;

        [field: SerializeField] public float DefaultSpeed { get; private set; }
        public float CurrentSpeed { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerCombo = GetComponent<PlayerCombo>();
            CurrentSpeed = DefaultSpeed;
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
            if (_playerCombo.CurrentState is IdleState)
            {
                CurrentSpeed = DefaultSpeed;
                return;
            }

            CurrentSpeed = 0;
            _rigidbody.velocity = Vector3.zero;
        }

        public void Move(float directionX, float directionZ)
        {
            if (_rigidbody.velocity.magnitude >= CurrentSpeed)
                return;

            var direction = new Vector3(directionX, _rigidbody.velocity.y, directionZ).normalized;
            _rigidbody.velocity = new Vector3(direction.x * CurrentSpeed, direction.y, direction.z * CurrentSpeed);
        }
    }
}