using System.Collections;
using Source.InputSource;
using Source.Player;
using Source.UI.Buttons.UIButtons;
using UnityEngine;

namespace Source.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class DeathScreen : GameScreen
    {
        [SerializeField] private RestartButton _restartButton;
        [SerializeField] private RegenerationButton _regenerationButton;
        [SerializeField] private ExitButton _exitButton;

        private PlayerHealth _playerHealth;
        private float _delay;
        private bool _isCoroutineStarted;

        private void OnEnable()
        {
            _regenerationButton.PlayerRegenerated += OnPlayerRegenerated;
        }

        private void OnDisable()
        {
            _regenerationButton.PlayerRegenerated += OnPlayerRegenerated;
            _playerHealth.Died -= OnDied;
        }

        private void Start()
        {
            _restartButton.transform.SetParent(transform);
            _regenerationButton.transform.SetParent(transform);
            _exitButton.transform.SetParent(transform);

            CanvasGroup.interactable = false;
        }

        public void Init(InputSwitcher inputSwitcher, PlayerHealth playerHealth, float delay)
        {
            base.Init(inputSwitcher);

            _playerHealth = playerHealth;
            _delay = delay;

            _playerHealth.Died += OnDied;
        }
        
        private void OnPlayerRegenerated()
        {
            Time.timeScale = 1;
            CanvasGroup.alpha = 0;

            CanvasGroup.interactable = false;

            SetObjectsActiveState(true);
        }
        
        private void OnDied()
        {
            if (_isCoroutineStarted == false)
                StartCoroutine(WaitTimeToOpen());
        }

        private IEnumerator WaitTimeToOpen()
        {
            _isCoroutineStarted = true;
            
            yield return new WaitForSeconds(_delay);
            
            Time.timeScale = 0;
            CanvasGroup.alpha = 1;

            CanvasGroup.interactable = true;

            _regenerationButton.SetInteractableState(_regenerationButton.IsClicked == false);
            SetObjectsActiveState(false);

            _isCoroutineStarted = false;
        }
    }
}