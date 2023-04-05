using Source.Player;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class BotDetectionTrigger : MonoBehaviour
    {
        [SerializeField] private float _viewRadius;
        
        private SphereCollider _sphereCollider;
        
        public bool IsPlayerInTrigger { get; private set; }

        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            _sphereCollider.radius = _viewRadius;
            _sphereCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
                IsPlayerInTrigger = true;
        }
    }
}