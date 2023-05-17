using System;
using Source.Player;
using UnityEngine;

namespace Source.Infrastructure
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _shootingEffect;
        
        private Transform _target;
        private Vector3 _targetPosition;
        
        private void Start()
        {
            if (_target == null)
                throw new ArgumentNullException();
            
            var targetPosition = _target.position;
            const int TargetYPosition = 1;

            _targetPosition = new Vector3(targetPosition.x, TargetYPosition, targetPosition.z);
            Instantiate(_shootingEffect, transform.position, Quaternion.identity, null);
        }

        private void Update()
        {
            if (_target.gameObject.activeSelf == false || _target == null)
                Destroy(gameObject);
            
            transform.position =
                Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            
            if(transform.position == _targetPosition)
                Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
                playerHealth.TryTakeDamage(_damage);

            Destroy(gameObject);
        }

        public void Init(Transform target)
        {
            _target = target;
        }
    }
}