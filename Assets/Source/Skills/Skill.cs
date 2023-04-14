using System.Collections;
using UnityEngine;

namespace Source.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        private Coroutine _coroutine;

        [field: SerializeField] public float Cost { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        public float ElapsedTime { get; private set; }

        protected void Initialization()
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
            while (ElapsedTime > 0)
            {
                ElapsedTime = Mathf.Clamp(ElapsedTime - Time.deltaTime, 0, float.MaxValue);
                yield return null;
            }

            ElapsedTime = Cooldown;
        }
    }
}