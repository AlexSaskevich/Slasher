using Source.Yandex;
using UnityEngine;

namespace Source.UI.Panels
{
    public sealed class EndScreen : GameScreen
    {
        [SerializeField] private AdShower _adShower;

        private void Start()
        {
            CanvasGroup.interactable = false;
        }

        public void End()
        {
            Time.timeScale = 0;
            CanvasGroup.alpha = 1;
            
            CanvasGroup.interactable = true;
            
            SetObjectsActiveState(false);
            
            //_adShower.Show();
        }
    }
}