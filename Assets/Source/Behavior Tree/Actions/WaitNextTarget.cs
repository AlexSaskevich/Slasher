using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using Source.Constants;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class WaitNextTarget : Action
    {
        public SharedFloat MinRandomTime;
        public SharedFloat MaxRandomTime;
        public SharedBotAnimator SharedBotAnimator;
        public SharedBotTarget SharedBotTarget;

        private float _time;
        private float _timer;
        
        public override void OnStart()
        {
            _time = Random.Range(MinRandomTime.Value, MaxRandomTime.Value);
        }

        public override TaskStatus OnUpdate()
        {
            _timer += Time.deltaTime;

            if (_timer >= _time)
            {
                _timer = 0;
                return TaskStatus.Success;
            }
            
            SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsRunning, false);
            SharedBotAnimator.Value.Animator.SetBool(AnimationConstants.IsWalking, false);
            
            return TaskStatus.Running;
        }
    }
}