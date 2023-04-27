using Source.Enums;
using UnityEngine;

namespace Source.GameLogic
{
    public static class GameProgressSaver
    {
        private const string Money = "Money";
        private const string Time = "Time";
        private const string ZombieScore = "ZombieScore";

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

        public static void SetZombieScore(int value)
        {
            PlayerPrefs.SetInt(ZombieScore, value);
        }

        public static int GetZombieScore()
        {
            return PlayerPrefs.GetInt(ZombieScore);
        }

        public static void SetGoodLevel(GoodStatus status, int level)
        {
            PlayerPrefs.SetInt(status.ToString(), level);
        }

        public static int GetGoodLevel(GoodStatus status)
        {
            return PlayerPrefs.GetInt(status.ToString());
        }

        public static void SetPlayerCharacteristic(CharacteristicStatus characteristicStatus, float value)
        {
            PlayerPrefs.SetFloat(characteristicStatus.ToString(), value);
        }

        public static float GetPlayerCharacteristic(CharacteristicStatus characteristicStatus)
        {
            return PlayerPrefs.GetFloat(characteristicStatus.ToString());
        }
    }
}