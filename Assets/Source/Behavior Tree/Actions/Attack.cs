using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using Source.Constants;

namespace Source.Behavior_Tree.Actions
{
    public sealed class Attack : Action
    {
        public SharedBotAttacker SharedBotAttacker;
        public SharedBotAnimator SharedBotAnimator;
        
        public override TaskStatus OnUpdate()
        {
            SharedBotAttacker.Value.Attack();
            SharedBotAnimator.Value.Animator.SetTrigger(AnimationConstants.Shoot);
            
            return TaskStatus.Success;
        }
    }
}