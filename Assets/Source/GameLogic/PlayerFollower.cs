using Source.Interfaces;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerFollower : MonoBehaviour, IMoveable
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset;

        [field: SerializeField] public float DefaultSpeed { get; private set; }

        private void LateUpdate()
        {
            Vector3 desiredPosition = _player.position + _offset;
            Move(desiredPosition.x, desiredPosition.z);
        }

        public void Move(float directionX, float directionZ)
        {
            Vector3 targetPosition = new(directionX, _offset.y, directionZ);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, DefaultSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}