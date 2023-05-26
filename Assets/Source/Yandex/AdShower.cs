using Source.GameLogic;
using UnityEngine;

namespace Source.Yandex
{
    public abstract class AdShower : MonoBehaviour
    {
        [SerializeField] private AudioListener _audioListener;

        private bool _isMuted;
        
        protected void OnOpenCallback()
        {
            Debug.LogWarning("Открылась реклама!!!");
            PauseGame();
        }

        protected void OnCloseCallback(bool isClosed)
        {
            Debug.LogWarning("Реклама закрылась!!!");
            ContinueGame();
        }

        protected void OnCloseCallback()
        {
            Debug.LogWarning("Реклама закрылась!!!");
            ContinueGame();
        }

        protected void OnErrorCallback(string errorMessage)
        {
            Debug.LogWarning("Сработал OnErrorCallback!!!");
            ContinueGame();
        }

        protected void OnOfflineCallback()
        {
            Debug.LogWarning("Сработал OnOfflineCallback!!!");
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