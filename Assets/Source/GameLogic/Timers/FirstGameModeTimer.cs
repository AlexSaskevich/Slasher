using System;

namespace Source.GameLogic.Timers
{
    public sealed class FirstGameModeTimer : Timer
    {
        private float _time;
        private readonly float _startTime;
        
        public FirstGameModeTimer(float time)
        {
            _time = time;
            _startTime = time;
        }

        public event Action Ended;
        
        public override void Update(float deltaTime)
        {
            _time -= deltaTime;

            if (_time > 0)
                return;
            
            Ended?.Invoke();
            _time = _startTime;
        }
    }
}