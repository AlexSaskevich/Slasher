using Source.GameLogic.Timers;
using Source.Yandex;
using UnityEngine;

namespace Source.UI.Panels
{
    public sealed class EndScreen : GameScreen
    {
        [SerializeField] private AdShower _adShower;
        [SerializeField] private FirstGameModeBlinder _firstGameModeBlinder;

        private FirstGameModeTimer _firstGameModeTimer;

        private void OnEnable()
        {
            _firstGameModeBlinder.Initialized += OnInitialized;
        }

        private void OnDisable()
        {
            _firstGameModeBlinder.Initialized -= OnInitialized;
            _firstGameModeTimer.Ended -= OnEnded;
        }

        private void Start()
        {
            CanvasGroup.interactable = false;
        }

        private void OnInitialized(FirstGameModeTimer firstGameModeTimer)
        {
            _firstGameModeTimer = firstGameModeTimer;
            _firstGameModeTimer.Ended += OnEnded;
        }

        private void OnEnded()
        {
            Time.timeScale = 0;
            CanvasGroup.alpha = 1;
            
            CanvasGroup.interactable = true;
            
            SetObjectsActiveState(false);
            
            _adShower.Show();
        }
    }
}