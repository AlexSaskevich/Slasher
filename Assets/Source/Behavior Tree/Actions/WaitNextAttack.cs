using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class WaitNextAttack : Action
    {
        public SharedBotMovement SharedBotMovement;
        public SharedBotTarget SharedBotTarget;
        public SharedBotAttacker SharedBotAttacker;

        private float _timer;

        public override TaskStatus OnUpdate()
        {
            _timer += Time.deltaTime;
            
            SharedBotMovement.Value.NavMeshAgent.destination = SharedBotTarget.Value.PlayerMovement.transform.position;

            return _timer >= SharedBotAttacker.Value.Delay ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}