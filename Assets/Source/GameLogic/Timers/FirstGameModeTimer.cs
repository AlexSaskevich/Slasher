using System;

namespace Source.GameLogic.Timers
{
    public sealed class FirstGameModeTimer : Timer
    {
        private float _time;
        private bool _isEnded;
        
        public FirstGameModeTimer(float time)
        {
            _time = time;
        }

        public event Action Ended;
        
        public override void Update(float deltaTime)
        {
            if (_isEnded)
                return;
            
            _time -= deltaTime;
            TrySetSeconds(Convert.ToInt32(_time - SecondsInMinute * Minutes));

            if (_time > 0)
                return;
            
            _isEnded = true;
            Ended?.Invoke();
        }

        public void TryIncreaseSeconds(int value)
        {
            if (value <= 0)
                return;
            
            _time += value;
        }
    }
}