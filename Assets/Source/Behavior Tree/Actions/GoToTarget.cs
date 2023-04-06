using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class GoToTarget : Action
    {
        private const int MinDistance = 1;
        
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;

        public override TaskStatus OnUpdate()
        {
            var targetPosition = SharedBotTarget.Value.CurrentTarget.transform.position;
            
            SharedBotMovement.Value.NavMeshAgent.destination = targetPosition;

            return Vector3.Distance(targetPosition, SharedBotMovement.Value.NavMeshAgent.transform.position) <= MinDistance
                ? TaskStatus.Success
                : TaskStatus.Running;
        }
    }
}