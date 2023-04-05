using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;

namespace Source.Behavior_Tree.Conditionals
{
    public sealed class SeekPlayer : Conditional
    {
        public SharedBotDetectionTrigger SharedBotDetectionTrigger;

        public override TaskStatus OnUpdate()
        {
            return SharedBotDetectionTrigger.Value.IsPlayerInTrigger ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}