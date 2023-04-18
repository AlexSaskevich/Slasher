using System;

namespace Source.GameLogic
{
    public sealed class Timer
    {
        private const int SecondsInMinute = 60;
        
        private static float s_highestScore;

        private float _time;
        private int _scoreSeconds;
        private int _scoreMinutes;
        
        public static string ScoreText { get; private set; }
        
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            Seconds = Convert.ToInt32(_time - SecondsInMinute * Minutes);

            if (Seconds > SecondsInMinute)
                Minutes++;

            if (s_highestScore >= _time) 
                return;
            
            s_highestScore = _time;
            _scoreSeconds = Seconds;
            _scoreMinutes = Minutes;

            ScoreText = _scoreSeconds.ToString().Length == 1
                ? $"{_scoreMinutes} : 0{_scoreSeconds}"
                : $"{_scoreMinutes} : {_scoreSeconds}";
        }

        public static string ConvertIntToTime(int value)
        {
            var minutes = value / SecondsInMinute;
            var seconds = value - minutes * SecondsInMinute;

            return seconds.ToString().Length == 1 ? $"{minutes} : 0{seconds}" : $"{minutes} : {seconds}";
        }

        public bool IsTimeHighest()
        {
            return _time > s_highestScore;
        }
    }
}