using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class Escape : Action
    {
        public SharedBotEscaper SharedBotEscaper;
        public SharedBotMovement SharedBotMovement;

        public override TaskStatus OnUpdate()
        {
            Vector3 escapeDirection = Vector3.Normalize(SharedBotMovement.Value.transform.position - SharedBotEscaper.Value.PlayerTransform.position);
            escapeDirection.y = 0;
            
            Vector3 newPosition = SharedBotMovement.Value.transform.position + escapeDirection;
            SharedBotMovement.Value.NavMeshAgent.SetDestination(newPosition);

            return TaskStatus.Running;
        }
    }
}