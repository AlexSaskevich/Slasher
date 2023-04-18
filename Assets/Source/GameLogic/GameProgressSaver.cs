using UnityEngine;

namespace Source.GameLogic
{
    public static class GameProgressSaver
    {
        private const string Money = "Money";
        private const string Time = "Time";

        public static void SetMoney(int money)
        {
            PlayerPrefs.SetInt(Money, money);
        }

        public static int GetMoney()
        {
            return PlayerPrefs.GetInt(Money);
        }

        public static void SetTime(string time)
        {
            PlayerPrefs.SetString(Time, time);
        }

        public static string GetTime()
        {
            return PlayerPrefs.GetString(Time);
        }
    }
}