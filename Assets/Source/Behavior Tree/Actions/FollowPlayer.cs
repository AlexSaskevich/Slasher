using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using Source.Constants;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class FollowPlayer : Action
    {
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;
        public SharedBotAttacker SharedBotAttacker;
        public SharedBotAnimator SharedBotAnimator;

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
                
                SharedBotAnimator.Value.Animator.SetTrigger(AnimationConstants.Shoot);
                
                SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsRunning, false);
                SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsWalking, false);
            }
            else
            {
                enemyNavMeshDestination = SharedBotTarget.Value.PlayerMovement.transform.position;
                
                SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsRunning, true);
            }

            SharedBotMovement.Value.NavMeshAgent.destination = enemyNavMeshDestination;

            if (_timer < SharedBotAttacker.Value.Delay || SharedBotAttacker.Value.IsPlayerDetected == false)
                return TaskStatus.Running;
            
            _timer = 0;
                
            return TaskStatus.Success;
        }
    }
}