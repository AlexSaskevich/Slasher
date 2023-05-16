using System;
using UnityEngine;

namespace Source.GameLogic.Timers
{
    public sealed class FirstGameModeBlinder : MonoBehaviour
    {
        [SerializeField] private float _time;

        public event Action<FirstGameModeTimer> Initialized;
        
        public FirstGameModeTimer FirstGameModeTimer { get; private set; }

        private void Start()
        {
            FirstGameModeTimer = new FirstGameModeTimer(_time);
            Initialized?.Invoke(FirstGameModeTimer);
        }

        private void Update()
        {
            FirstGameModeTimer.Update(Time.deltaTime);
        }
    }
}