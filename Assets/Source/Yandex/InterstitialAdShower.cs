using Agava.YandexGames;

namespace Source.Yandex
{
    public sealed class InterstitialAdShower : AdShower
    {
        public override void Show()
        {
            InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack);
        }
    }
}