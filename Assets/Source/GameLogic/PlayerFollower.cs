﻿using Source.Interfaces;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerFollower : MonoBehaviour, IMoveable
    {
        [SerializeField] private Transform _player;

        private Vector3 _playerPosition;
        private Vector3 _position;
        private Vector3 _offence;

        [field: SerializeField] public float Speed { get; private set; }
        
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

            Move(new Vector3(_playerPosition.x + _offence.x, _position.y,
                _playerPosition.z + _offence.z));
        }

        public void Move(Vector3 direction)
        {
            transform.position = direction;
        }
    }
}