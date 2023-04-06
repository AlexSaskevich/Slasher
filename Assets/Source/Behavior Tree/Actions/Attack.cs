using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;

namespace Source.Behavior_Tree.Actions
{
    public sealed class Attack : Action
    {
        public SharedBotAttacker SharedBotAttacker;
        
        public override TaskStatus OnUpdate()
        {
            SharedBotAttacker.Value.Attack();
            return TaskStatus.Success;
        }
    }
}