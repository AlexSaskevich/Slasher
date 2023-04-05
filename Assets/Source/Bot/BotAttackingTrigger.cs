using System;
using Source.Player;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class BotAttackingTrigger : MonoBehaviour
    {
        private SphereCollider _sphereCollider;

        public event Action PlayerDetected;
        public event Action PlayerLeft;
        
        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            _sphereCollider.isTrigger = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
                PlayerDetected?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out PlayerMovement playerMovement))
                PlayerLeft?.Invoke();
        }

        public void Init(float radius)
        {
            if (radius <= 0)
                throw new ArgumentNullException();

            _sphereCollider.radius = radius;
        }
    }
}