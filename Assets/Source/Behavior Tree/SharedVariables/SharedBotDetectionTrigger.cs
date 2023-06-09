﻿using BehaviorDesigner.Runtime;
using Source.Bot;

namespace Source.Behavior_Tree.SharedVariables
{
    public sealed class SharedBotDetectionTrigger : SharedVariable<BotDetectionTrigger>
    {
        public static implicit operator SharedBotDetectionTrigger(BotDetectionTrigger botDetectionTrigger) =>
            new() { Value = botDetectionTrigger };
    }
}