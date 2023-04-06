using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class FollowPlayer : Action
    {
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;
        public SharedBotAttacker SharedBotAttacker;

        private float _timer;

        public override TaskStatus OnUpdate()
        {
            _timer += Time.deltaTime;

            Vector3 enemyNavMeshDestination;
            
            if (SharedBotAttacker.Value.IsPlayerDetected)
            {
                var enemyTransform = SharedBotMovement.Value.NavMeshAgent.transform;

                var enemyPosition = enemyTransform.position;
                var playerPosition = SharedBotTarget.Value.PlayerMovement.transform.position;

                enemyTransform.LookAt(new Vector3(playerPosition.x, enemyPosition.y, playerPosition.z));
                
                enemyNavMeshDestination = enemyPosition;
            }
            else
            {
                enemyNavMeshDestination = SharedBotTarget.Value.PlayerMovement.transform.position;
            }

            SharedBotMovement.Value.NavMeshAgent.destination = enemyNavMeshDestination;

            if (_timer < SharedBotAttacker.Value.Delay || SharedBotAttacker.Value.IsPlayerDetected == false)
                return TaskStatus.Running;
            
            _timer = 0;
                
            return TaskStatus.Success;
        }
    }
}