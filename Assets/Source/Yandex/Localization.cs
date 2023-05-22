using Agava.YandexGames;
using Lean.Localization;
using Source.Enums;
using UnityEngine;

namespace Source.Yandex
{
    public sealed class Localization : MonoBehaviour
    {
        private const string EnglishCode = "en";
        private const string RussianCode = "ru";
        private const string TurkishCode = "tr";

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            string language = languageCode switch
            {
                EnglishCode => Language.English.ToString(),
                RussianCode => Language.Russian.ToString(),
                TurkishCode => Language.Turkish.ToString(),
                _ => Language.English.ToString()
            };

            LeanLocalization.SetCurrentLanguageAll(language);
        }
    }
}