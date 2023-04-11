using BehaviorDesigner.Runtime.Tasks;
using Source.Behavior_Tree.SharedVariables;
using UnityEngine;

namespace Source.Behavior_Tree.Actions
{
    public sealed class Escape : Action
    {
        public SharedBotEscaper SharedBotEscaper;
        //public SharedBotMovement SharedBotMovement;

        public override TaskStatus OnUpdate()
        {
            // убегать от игрока
            //SharedBotEscaper.Value.Player

            transform.LookAt(SharedBotEscaper.Value.Player);
            Debug.Log("Убегает");

            //Vector3 positionDifference = SharedBotEscaper.Value.Player.position - SelfMob.Value.transform.position;
            //positionDifference.y = 0f;
            //Vector3 fleeDirection = Vector3.Normalize(-positionDifference);
            //SelfMob.Value.Move(fleeDirection * MovementSpeed.Value);

            return TaskStatus.Running;
        }
    }
}