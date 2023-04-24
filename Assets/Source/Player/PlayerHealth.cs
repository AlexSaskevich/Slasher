using Source.GameLogic;
using Source.InputSource;
using Source.Interfaces;
using System;

namespace Source.Player
{
    public sealed class PlayerHealth : Health, IBuffable
    {
        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;
        private float _healModifier;
        private float _chanceAvoidDamage;

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
            _chanceAvoidDamage = 0;
        }

        private void Update()
        {
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

            if (_chanceAvoidDamage > 0)
                damage *= GetFinalDamage();

            base.TryTakeDamage(damage);
        }

        public void AddModifier(float modifier)
        {
            if (modifier <= 0)
                throw new ArgumentException();

            if (modifier <= 1)
                _chanceAvoidDamage += modifier;
            else
                _healModifier += modifier;

            IsBuffed = true;
        }

        public void RemoveModifier(float modifier)
        {
            if (modifier <= 0)
                throw new ArgumentException();

            if (modifier <= 1)
                _chanceAvoidDamage -= modifier;
            else
                _healModifier -= modifier;

            IsBuffed = false;
        }

        private float GetFinalDamage()
        {
            const float MaxProbability = 1.0f;
            const float MinProbability = 0.0f;
            var randomProbability = UnityEngine.Random.Range(MinProbability, MaxProbability);

            return randomProbability > MaxProbability - _chanceAvoidDamage ? MinProbability : MaxProbability;
        }
    }
}