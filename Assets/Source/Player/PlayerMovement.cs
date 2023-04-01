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

        public void Move(Vector3 direction)
        {
            _rigidbody.velocity = new Vector3(direction.x * Speed, _rigidbody.velocity.y, direction.z * Speed);
        }
    }
}