using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class WaitNextAttack : Action
    {
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;
        public SharedBotAttacker SharedBotAttacker;

        private float _timer;

        public override TaskStatus OnUpdate()
        {
            _timer += Time.deltaTime;
            
            var enemyNavMeshDestination = SharedBotAttacker.Value.IsPlayerDetected
                ? SharedBotMovement.Value.NavMeshAgent.transform.position
                : SharedBotTarget.Value.PlayerMovement.transform.position;

            SharedBotMovement.Value.NavMeshAgent.destination = enemyNavMeshDestination;

            if (_timer >= SharedBotAttacker.Value.Delay)
            {
                _timer = 0;
                
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}