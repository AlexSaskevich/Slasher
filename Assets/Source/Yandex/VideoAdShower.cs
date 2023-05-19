using Agava.YandexGames;

namespace Source.Yandex
{
    public sealed class VideoAdShower : AdShower
    {
        public override void Show()
        {
            VideoAd.Show(OnOpenCallback, OnCloseCallback, onErrorCallback: OnErrorCallback);
        }
    }
}