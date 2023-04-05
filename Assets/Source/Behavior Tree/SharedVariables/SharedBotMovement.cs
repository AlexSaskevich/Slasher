using BehaviorDesigner.Runtime;
using Source.Bot;

namespace Source.Behavior_Tree.SharedVariables
{
    public sealed class SharedBotMovement : SharedVariable<BotMovement>
    {
        public static implicit operator SharedBotMovement(BotMovement botMovement) => new() { Value = botMovement };
    }
}