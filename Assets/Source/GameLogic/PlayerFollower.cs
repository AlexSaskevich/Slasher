using Source.Interfaces;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerFollower : MonoBehaviour, IMoveable
    {
        [SerializeField] private Transform _player;

        private Vector3 _offence;
        private Vector3 _position;
        private Vector3 _playerPosition;

        public float DefaultSpeed => 1;

        private void Start()
        {
            _playerPosition = _player.position;
            _position = transform.position;

            _offence = new Vector3(_position.x - _playerPosition.x, 0, _position.z - _playerPosition.z);
        }

        private void LateUpdate()
        {
            _playerPosition = _player.position;
            _position = transform.position;

            Move(_playerPosition.x + _offence.x, _playerPosition.z + _offence.z);
        }

        public void Move(float directionX, float directionZ)
        {
            transform.position = new Vector3(directionX, transform.position.y, directionZ);
        }
    }
}