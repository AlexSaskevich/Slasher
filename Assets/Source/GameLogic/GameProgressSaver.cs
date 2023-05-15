using Source.Enums;
using UnityEngine;

namespace Source.GameLogic
{
    public static class GameProgressSaver
    {
        private const string Money = "Money";
        private const string TimeText = "TimeText";
        private const string Time = "Time";
        private const string ZombieScore = "ZombieScore";
        private const string PlayerCharacter = "PlayerCharacter";
        private const string TimeModeScore = "TimeModeScore";

        public static void SetMoney(int money)
        {
            PlayerPrefs.SetInt(Money, money);
        }

        public static int GetMoney()
        {
            return PlayerPrefs.GetInt(Money);
        }

        public static void SetTime(float time)
        {
            PlayerPrefs.SetFloat(Time, time);
        }

        public static float GetTime()
        {
            return PlayerPrefs.GetFloat(Time);
        }
        
        public static void SetTimeText(string time)
        {
            PlayerPrefs.SetString(TimeText, time);
        }

        public static string GetTimeText()
        {
            return PlayerPrefs.GetString(TimeText);
        }

        public static void SetZombieScore(int value)
        {
            PlayerPrefs.SetInt(ZombieScore, value);
        }

        public static int GetZombieScore()
        {
            return PlayerPrefs.GetInt(ZombieScore);
        }

        public static void SetBoosterLevel(PlayerCharacterName playerCharacterName, GoodStatus status, int level)
        {
            PlayerPrefs.SetInt(playerCharacterName + status.ToString(), level);
        }

        public static int GetBoosterLevel(PlayerCharacterName playerCharacterName, GoodStatus status)
        {
            return PlayerPrefs.GetInt(playerCharacterName + status.ToString());
        }

        public static void SetPlayerCharacteristic(PlayerCharacterName playerCharacterName,
            CharacteristicStatus characteristicStatus, float value)
        {
            PlayerPrefs.SetFloat(playerCharacterName + characteristicStatus.ToString(), value);
        }

        public static float GetPlayerCharacteristic(PlayerCharacterName playerCharacterName,
            CharacteristicStatus characteristicStatus)
        {
            return PlayerPrefs.GetFloat(playerCharacterName + characteristicStatus.ToString());
        }

        public static void SetCurrentCharacterIndex(int playerCharacterName)
        {
            PlayerPrefs.SetInt(PlayerCharacter, playerCharacterName);
        }

        public static int GetCurrentCharacterIndex()
        {
            return PlayerPrefs.GetInt(PlayerCharacter);
        }

        public static void SetCharacterBoughtStatus(PlayerCharacterName playerCharacterName, bool isBought)
        {
            var boughtStatus = isBought ? 1 : 0;

            PlayerPrefs.SetInt(playerCharacterName.ToString(), boughtStatus);
        }

        public static bool GetCharacterBoughtStatus(PlayerCharacterName playerCharacterName)
        {
            var boughtStatus = PlayerPrefs.GetInt(playerCharacterName.ToString());

            return boughtStatus != 0;
        }

        public static void SetTimeModeScore(int score)
        {
            PlayerPrefs.SetInt(TimeModeScore, score);
        }

        public static int GetTimeModeScore()
        {
            return PlayerPrefs.GetInt(TimeModeScore);
        }
    }
}