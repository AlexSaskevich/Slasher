namespace Source.GameLogic.Timers
{
    public abstract class Timer
    {
        protected const int SecondsInMinute = 60;
        
        public static string ScoreText { get; private set; }
        
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        
        public static string ConvertIntToTime(int value)
        {
            var minutes = value / SecondsInMinute;
            var seconds = value - minutes * SecondsInMinute;

            return seconds.ToString().Length == 1 ? $"{minutes} : 0{seconds}" : $"{minutes} : {seconds}";
        }

        protected void SetScoreText(string scoreText)
        {
            if (string.IsNullOrEmpty(scoreText))
                return;

            ScoreText = scoreText;
        }
        
        protected void TrySetSeconds(int seconds)
        {
            if (seconds < 0)
                return;
            
            Seconds = seconds;
        }

        protected void ResetSeconds()
        {
            Seconds = 0;
        }

        protected void SetMinutes(int minutes)
        {
            if (minutes <= Minutes)
                return;

            Minutes = minutes;
        }

        public abstract void Update(float deltaTime);
    }
}