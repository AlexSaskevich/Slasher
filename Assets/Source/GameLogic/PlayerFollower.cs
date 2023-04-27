using Source.Interfaces;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerFollower : MonoBehaviour, IMoveable
    {
        [SerializeField] private Vector3 _offset;

        private Transform _player;
        
        [field: SerializeField] public float DefaultSpeed { get; private set; }

        private void LateUpdate()
        {
            var desiredPosition = _player.position + _offset;
            Move(desiredPosition.x, desiredPosition.z);
        }

        public void Move(float directionX, float directionZ)
        {
            var targetPosition = new Vector3(directionX, _offset.y, directionZ);
            var smoothedPosition = Vector3.Lerp(transform.position, targetPosition, DefaultSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }

        public void Init(Transform player)
        {
            _player = player;
        }
    }
}