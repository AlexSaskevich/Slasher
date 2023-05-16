namespace Source.Yandex
{
    public sealed class LeaderboardPlayer
    {
        public LeaderboardPlayer(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public string Name { get; private set; }
        public int Score { get; private set; }
    }
}