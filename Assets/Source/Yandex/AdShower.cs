using Source.GameLogic;
using System;
using UnityEngine;

namespace Source.Yandex
{
    public abstract class AdShower : MonoBehaviour
    {
        [SerializeField] private AudioListener _audioListener;

        private bool _isMuted;

        public event Action AdvertisementClosed;

        protected void OnOpenCallback()
        {
            PauseGame();
        }

        protected void OnCloseCallback(bool isClosed)
        {
            ContinueGame();

            Time.timeScale = 0;
        }

        protected void OnCloseCallback()
        {
            ContinueGame();

            AdvertisementClosed?.Invoke();
        }

        protected void OnErrorCallback(string errorMessage)
        {
            ContinueGame();
        }

        protected void OnOfflineCallback()
        {
            ContinueGame();
        }

        private void ContinueGame()
        {
            Time.timeScale = 1;

            if (_isMuted)
                return;

            _audioListener.enabled = true;
            SoundMuter.Unmute();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;

            _isMuted = SoundMuter.IsMuted;

            _audioListener.enabled = false;
            SoundMuter.Mute();
        }

        public abstract void Show();
    }
}