using BehaviorDesigner.Runtime;
using Source.Bot;

namespace Source.Behavior_Tree.SharedVariables
{
    public sealed class SharedBotEscaper : SharedVariable<BotEscaper>
    {
        public static implicit operator SharedBotEscaper(BotEscaper botEscaper) => new() { Value = botEscaper };
    }
}