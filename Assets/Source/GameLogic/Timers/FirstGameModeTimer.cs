using System;

namespace Source.GameLogic.Timers
{
    public sealed class FirstGameModeTimer : Timer
    {
        private readonly float _startTime;
        
        private float _time;
        
        public FirstGameModeTimer(float time)
        {
            _time = time;
            _startTime = time;
        }

        public event Action Ended;
        
        public override void Update(float deltaTime)
        {
            _time -= deltaTime;
            TrySetSeconds(Convert.ToInt32(_time - SecondsInMinute * Minutes));
                
            if (_time > 0)
                return;
            
            Ended?.Invoke();
            _time = _startTime;
        }
    }
}