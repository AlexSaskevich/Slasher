using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;

namespace Source.Behavior_Tree.Actions
{
    public sealed class IsPlayerInTrigger : Conditional
    {
        public SharedBotDetectionTrigger SharedBotDetectionTrigger;

        public override TaskStatus OnUpdate()
        {
            return SharedBotDetectionTrigger.Value.IsPlayerInTrigger ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}