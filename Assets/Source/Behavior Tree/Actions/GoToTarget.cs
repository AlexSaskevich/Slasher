using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class GoToTarget : Action
    {
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;

        public override TaskStatus OnUpdate()
        {
            var targetPosition = SharedBotTarget.Value.CurrentTarget.transform.position;
            
            SharedBotMovement.Value.NavMeshAgent.destination = targetPosition;

            if (Vector3.Distance(targetPosition, SharedBotMovement.Value.NavMeshAgent.transform.position) <= 1)
                return TaskStatus.Success;

            return TaskStatus.Running;
        }
    }
}