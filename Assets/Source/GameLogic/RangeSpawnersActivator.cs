using System;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class RangeSpawnersActivator : MonoBehaviour
    {
        [SerializeField] private float _timeToActivate;

        private float _timer;

        public event Action<bool> Activated; 

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < _timeToActivate)
                return;
            
            const bool IsActivating = true;
                
            Activated?.Invoke(IsActivating);
            Destroy(gameObject);
        }
    }
}