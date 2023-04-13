using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using Source.Constants;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class Escape : Action
    {
        public SharedBotEscaper SharedBotEscaper;
        public SharedBotMovement SharedBotMovement;
        public SharedBotAnimator SharedBotAnimator;
        public SharedFloat NewSpeed;

        public override void OnStart()
        {
            SharedBotMovement.Value.NavMeshAgent.speed = NewSpeed.Value;
        }

        public override TaskStatus OnUpdate()
        {
            Vector3 escapeDirection = Vector3.Normalize(SharedBotMovement.Value.transform.position - SharedBotEscaper.Value.PlayerTransform.position);
            escapeDirection.y = 0;
            
            Vector3 newPosition = SharedBotMovement.Value.transform.position + escapeDirection;
            SharedBotMovement.Value.NavMeshAgent.SetDestination(newPosition);

            SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsRunning, true);
            
            return TaskStatus.Running;
        }
    }
}