using Source.Combo;
using System;
using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    [RequireComponent(typeof(Animator), typeof(PlayerCombo))]
    public abstract class Skill : MonoBehaviour
    {
        private Coroutine _coroutine;

        public event Action TimerStarted;

        [field: SerializeField] public float Cost { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        public float ElapsedTime { get; private set; }
        protected Animator Animator { get; private set; }
        protected PlayerCombo PlayerCombo { get; private set; }

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            PlayerCombo = GetComponent<PlayerCombo>();
        }

        protected virtual void Start()
        {
            ElapsedTime = Cooldown;
        }

        public abstract void TryActivate();

        protected void StartTimer()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(CalculateTime());
        }

        private IEnumerator CalculateTime()
        {
            TimerStarted?.Invoke();

            while (ElapsedTime > 0)
            {
                ElapsedTime = Mathf.Clamp(ElapsedTime - Time.deltaTime, 0, float.MaxValue);
                yield return null;
            }

            ElapsedTime = Cooldown;
        }
    }
}