using Source.GameLogic;
using UnityEngine;

namespace Source.Yandex
{
    public abstract class AdShower : MonoBehaviour
    {
        [SerializeField] private AudioListener _audioListener;
        
        protected void OnOpenCallback()
        {
            PauseGame();
        }

        protected void OnCloseCallback(bool isClosed)
        {
            ContinueGame();
        }

        protected void OnCloseCallback()
        {
            ContinueGame();
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

            _audioListener.enabled = true;
            SoundMuter.Unmute();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;

            _audioListener.enabled = false;
            SoundMuter.Mute();
        }

        public abstract void Show();
    }
}