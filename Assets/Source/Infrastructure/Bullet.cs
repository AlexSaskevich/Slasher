using Source.Player;
using UnityEngine;

namespace Source.Infrastructure
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        
        private Transform _target;

        private void Update()
        {
            if(_target.gameObject.activeSelf == false || _target == null)
                Destroy(gameObject);
            
            transform.position =
                Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
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