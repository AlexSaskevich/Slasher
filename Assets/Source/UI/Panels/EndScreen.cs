using Source.GameLogic.Timers;
using UnityEngine;

namespace Source.UI.Panels
{
    public sealed class EndScreen : GameScreen
    {
        [SerializeField] private FirstGameModeBlinder _firstGameModeBlinder;

        private void OnEnable()
        {
            _firstGameModeBlinder.FirstGameModeTimer.Ended += OnEnded;
        }

        private void OnDisable()
        {
            _firstGameModeBlinder.FirstGameModeTimer.Ended -= OnEnded;
        }

        private void Start()
        {
            CanvasGroup.interactable = false;
        }

        private void OnEnded()
        {
            Time.timeScale = 0;
            CanvasGroup.alpha = 1;
            
            CanvasGroup.interactable = true;
            
            SetObjectsActiveState(false);
        }
    }
}