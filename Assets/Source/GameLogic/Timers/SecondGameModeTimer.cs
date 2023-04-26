using System;

namespace Source.GameLogic.Timers
{
    public sealed class SecondGameModeTimer : Timer
    {
        private static float s_highestScore;

        private float _time;
        private int _scoreSeconds;
        private int _scoreMinutes;
        
        public override void Update(float deltaTime)
        {
            _time += deltaTime;

            SetSeconds(Convert.ToInt32(_time - SecondsInMinute * Minutes));

            if (Seconds > SecondsInMinute)
                SetMinutes(Minutes + 1);

            if (s_highestScore >= _time) 
                return;
            
            s_highestScore = _time;
            _scoreSeconds = Seconds;
            _scoreMinutes = Minutes;

            SetScoreText(_scoreSeconds.ToString().Length == 1
                ? $"{_scoreMinutes} : 0{_scoreSeconds}"
                : $"{_scoreMinutes} : {_scoreSeconds}");
        }

        public bool IsTimeHighest()
        {
            return _time > s_highestScore;
        }
    }
}