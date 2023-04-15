using System.Linq;
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
            var availableTargets =
                SharedBotTarget.Value.Targets.Where(target => target != SharedBotTarget.Value.CurrentTarget).ToArray();

            SharedBotTarget.Value.CurrentTarget = availableTargets[Random.Range(0, availableTargets.Length)];
            
            return SharedBotTarget.Value.CurrentTarget == null ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}