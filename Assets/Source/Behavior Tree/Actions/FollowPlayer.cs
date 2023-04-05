using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;

namespace Source.Behavior_Tree.Actions
{
    public sealed class FollowPlayer : Action
    {
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;
        public SharedBotAttacker SharedBotAttacker;

        public override TaskStatus OnUpdate()
        {
            SharedBotMovement.Value.NavMeshAgent.destination = SharedBotTarget.Value.PlayerMovement.transform.position;

            return SharedBotAttacker.Value.IsPlayerDetected ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}