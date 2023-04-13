﻿using Source.Constants;
using Source.Player;
using UnityEngine;

namespace Source.Combo
{
    public sealed class EntryState : State
    {
        public override void Enter(PlayerCombo playerCombo)
        {
            playerCombo.Animator.SetTrigger(AnimationConstants.Attack1);
        }

        public override void Update(PlayerCombo playerCombo, PlayerInput playerInput)
        {
            if (IsCurrentAnimationName(playerCombo.Animator, AnimationConstants.Attack1) == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator))
                playerCombo.SwitchState(new MoveState());

            if (playerInput.IsAttackButtonClicked == false)
                return;

            if (CheckCurrentAnimationEnd(playerCombo.Animator, AnimationConstants.SwitchAnimationTime) && IsEnoughAgility(playerCombo, AnimationConstants.HitCount2))
                playerCombo.SwitchState(new ComboState());
        }

        protected override bool CheckCurrentAnimationEnd(Animator animator, float animationEndTime = AnimationConstants.EndAnimationTime)
        {
            return animator.GetCurrentAnimatorStateInfo(1).normalizedTime > animationEndTime;
        }

        protected override bool IsCurrentAnimationName(Animator animator, string name)
        {
            return animator.GetCurrentAnimatorStateInfo(1).IsName(name);
        }
    }
}