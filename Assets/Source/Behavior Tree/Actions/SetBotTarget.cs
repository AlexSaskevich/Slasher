using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Behavior_Tree.Actions
{
    public sealed class SetBotTarget : Action
    {
        public SharedBotTarget SharedBotTarget;

        public override TaskStatus OnUpdate()
        {
            SharedBotTarget.Value.CurrentTarget =
                SharedBotTarget.Value.Targets[Random.Range(0, SharedBotTarget.Value.Targets.Length)];
            
            Debug.Log(SharedBotTarget.Value.CurrentTarget.name);

            return SharedBotTarget.Value.CurrentTarget == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}