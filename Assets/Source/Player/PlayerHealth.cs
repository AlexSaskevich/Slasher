﻿using Source.GameLogic;
using Source.InputSource;
using Source.Interfaces;
using System;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerHealth : Health, IBuffable
    {
        private const float MaxChanceAvoidDamage = 1f;
        private const float MinChangeAvoidDamage = 0f;

        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;
        private float _healModifier;

        public float ChanceAvoidDamage { get; private set; }
        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _inputSwitcher = GetComponent<InputSwitcher>();
        }

        protected override void Start()
        {
            base.Start();
            _inputSource = _inputSwitcher.InputSource;
            _healModifier = 0;
            ChanceAvoidDamage = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
                TryTakeDamage(10);

            if (IsBuffed == false)
                return;

            if (_healModifier > 0)
                TryHeal(_healModifier);
        }

        protected override void Die()
        {
            _inputSource.Disable();
        }

        public override void TryTakeDamage(float damage)
        {
            if (enabled == false)
                return;

            switch (ChanceAvoidDamage)
            {
                case MaxChanceAvoidDamage:
                    if (CurrentHealth - damage <= 0)
                        return;
                    break;
                default:
                    damage *= GetFinalDamage();
                    break;
            }

            base.TryTakeDamage(damage);
        }

        public void AddModifier(float modifier)
        {
            if (modifier <= 0)
                throw new ArgumentException();

            if (modifier <= 1)
                ChanceAvoidDamage += modifier;
            else
                _healModifier += modifier;

            IsBuffed = true;
        }

        public void RemoveModifier(float modifier)
        {
            if (modifier <= 0)
                throw new ArgumentException();

            if (modifier <= 1)
                ChanceAvoidDamage -= modifier;
            else
                _healModifier -= modifier;

            IsBuffed = false;
        }

        private float GetFinalDamage()
        {
            var randomProbability = UnityEngine.Random.Range(MinChangeAvoidDamage, MaxChanceAvoidDamage);

            return randomProbability > MaxChanceAvoidDamage - ChanceAvoidDamage ? MinChangeAvoidDamage : MaxChanceAvoidDamage;
        }
    }
}