using System;

namespace Source.GameLogic.Timers
{
    public sealed class SecondGameModeTimer : Timer
    {
        private readonly int _border;

        private float _time;
        private int _scoreSeconds;
        private int _scoreMinutes;

        public SecondGameModeTimer(int border, float highestScore)
        {
            _border = border;
            HighestScore = highestScore;
        }

        public event Action BorderReached;
        
        public static float HighestScore { get; private set; }
        
        public override void Update(float deltaTime)
        {
            _time += deltaTime;
            TrySetSeconds(Convert.ToInt32(_time - SecondsInMinute * Minutes));

            if (Seconds % _border == 0 && Seconds != 0)
                BorderReached?.Invoke();

            if (Seconds < SecondsInMinute)
                return;
            
            SetMinutes(Minutes + 1);
            ResetSeconds();
        }

        public void SetHighestScore()
        {
            if (HighestScore >= _time)
                return;

            HighestScore = _time;
            _scoreSeconds = Seconds;
            _scoreMinutes = Minutes;

            SetScoreText(_scoreSeconds.ToString().Length == 1
                ? $"{_scoreMinutes} : 0{_scoreSeconds}"
                : $"{_scoreMinutes} : {_scoreSeconds}");
        }

        public bool IsTimeHighest()
        {
            return _time > HighestScore;
        }
    }
}