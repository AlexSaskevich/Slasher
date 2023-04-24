using Source.GameLogic;
using Source.InputSource;
using Source.Interfaces;
using System.Collections;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerHealth : Health, IBuffable
    {
        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;
        private Coroutine _coroutine;

        public bool IsBuffed { get; private set; }

        private void Awake()
        {
            _inputSwitcher = GetComponent<InputSwitcher>();
        }

        protected override void Start()
        {
            base.Start();
            _inputSource = _inputSwitcher.InputSource;
        }

        protected override void Die()
        {
            _inputSource.Disable();
        }

        public override void TryTakeDamage(float damage)
        {
            if (enabled == false)
                return;

            base.TryTakeDamage(damage);
        }

        public void AddModifier(float modifier)
        {
            IsBuffed = true;

            StartHeal(modifier);
        }

        public void RemoveModifier(float modifier)
        {
            IsBuffed = false;
        }

        private void StartHeal(float modifier)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(HealCoroutine(modifier));
        }

        private IEnumerator HealCoroutine(float modifier)
        {
            while (IsBuffed)
            {
                TryHeal(modifier);
                yield return null;
            }
        }
    }
}