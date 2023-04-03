using Source.Interfaces;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerMovement : MonoBehaviour, IMoveable
    {
        [field: SerializeField] public float Speed { get; private set; }

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(float directionX, float directionZ)
        {
            if (_rigidbody.velocity.magnitude >= Speed)
                return;

            _rigidbody.velocity = new Vector3(directionX * Speed, _rigidbody.velocity.y, directionZ * Speed);
        }
    }
}