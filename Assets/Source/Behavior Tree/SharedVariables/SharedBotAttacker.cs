using BehaviorDesigner.Runtime;
using Source.Bot;

namespace Source.Behavior_Tree.SharedVariables
{
    public sealed class SharedBotAttacker : SharedVariable<BotAttacker>
    {
        public static implicit operator SharedBotAttacker(BotAttacker botAttacker) => new() { Value = botAttacker };
    }
}