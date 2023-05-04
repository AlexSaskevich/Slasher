using UnityEngine;

namespace Source.Yandex
{
    public abstract class AdShower : MonoBehaviour
    {
        [SerializeField] private AudioListener _audioListener;
        
        protected void OnOpenCallBack()
        {
            Time.timeScale = 0;
            
            _audioListener.enabled = false;
        }

        protected void OnCloseCallBack()
        {
            Time.timeScale = 1;

            _audioListener.enabled = true;
        }

        protected void OnCloseCallBack(bool state)
        {
            if (state)
                return;

            Time.timeScale = 1;

            _audioListener.enabled = true;
        }

        public abstract void Show();
    }
}