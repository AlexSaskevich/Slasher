using BehaviorDesigner.Runtime;
using Source.Bot;

namespace Source.Behavior_Tree.SharedVariables
{
    public sealed class SharedBotAnimator : SharedVariable<BotAnimator>
    {
        public static implicit operator SharedBotAnimator(BotAnimator botAnimator) => new() { Value = botAnimator };
    }
}