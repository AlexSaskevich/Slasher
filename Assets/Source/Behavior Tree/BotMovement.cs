using UnityEngine;
using UnityEngine.AI;

namespace Source.Behavior_Tree
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class BotMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
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