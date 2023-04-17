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
        public SharedBotDetectionTrigger SharedBotDetectionTrigger;
        public SharedFloat NewSpeed;
        public SharedFloat TimeToReturnInWalkState;

        private float _timer;
        
        public override void OnStart()
        {
            SharedBotMovement.Value.NavMeshAgent.speed = NewSpeed.Value;

            _timer = 0;
        }

        public override TaskStatus OnUpdate()
        {
            if (SharedBotDetectionTrigger.Value.IsPlayerLeft)
                _timer += Time.deltaTime;

            if (_timer >= TimeToReturnInWalkState.Value)
            {
                _timer = 0;

                SharedBotDetectionTrigger.Value.ResetValues();
            }
            
            var botPosition = SharedBotMovement.Value.transform.position;
            var escapeDirection = Vector3.Normalize(botPosition - SharedBotEscaper.Value.PlayerTransform.position);
            var newPosition = botPosition + escapeDirection;
            
            escapeDirection.y = 0;
            
            SharedBotMovement.Value.NavMeshAgent.SetDestination(newPosition);

            SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsRunning, true);
            
            return TaskStatus.Running;
        }
    }
}