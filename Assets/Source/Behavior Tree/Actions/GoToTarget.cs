using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using Source.Constants;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class GoToTarget : Action
    {
        private const float MinDistance = 0.5f;
        
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;
        public SharedBotAnimator SharedBotAnimator;

        public override TaskStatus OnUpdate()
        {
            var targetPosition = SharedBotTarget.Value.CurrentTarget.transform.position;
            
            SharedBotMovement.Value.NavMeshAgent.destination = targetPosition;

            SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsWalking, true);
            
            return Vector3.Distance(targetPosition, SharedBotMovement.Value.NavMeshAgent.transform.position) <= MinDistance
                ? TaskStatus.Success
                : TaskStatus.Running;
        }
    }
}