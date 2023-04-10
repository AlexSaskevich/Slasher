using Source.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Bot
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class BotMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        [field: SerializeField] public BotStatus BotStatus { get; private set; }
        
        public NavMeshAgent NavMeshAgent { get; private set; }

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            NavMeshAgent.speed = _speed;
        }
    }
}