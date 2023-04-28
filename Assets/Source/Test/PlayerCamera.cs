using UnityEngine;

namespace Source.Test
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        [SerializeField] private Transform _player;

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
    }
}