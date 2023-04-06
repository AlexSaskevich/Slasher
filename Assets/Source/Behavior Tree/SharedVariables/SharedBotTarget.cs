using System;
using BehaviorDesigner.Runtime;
using Source.Bot;

namespace Source.Behavior_Tree.SharedVariables
{
    [Serializable]
    public sealed class SharedBotTarget : SharedVariable<BotTarget>
    {
        public static implicit operator SharedBotTarget(BotTarget botTarget) => new() { Value = botTarget };
    }
}