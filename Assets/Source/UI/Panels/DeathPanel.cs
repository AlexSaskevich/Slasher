using System.Collections;
using Source.InputSource;
using Source.Player;
using Source.UI.Buttons.UIButtons;
using UnityEngine;

namespace Source.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class DeathPanel : MonoBehaviour
    {
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private RestartButton _restartButton;
        [SerializeField] private RegenerationButton _regenerationButton;
        
        private CanvasGroup _canvasGroup;
        private PlayerHealth _playerHealth;
        private InputSwitcher _inputSwitcher;
        private float _delay;
        private bool _isCoroutineStarted;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0;
        }

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
            
            _restartButton.SetInteractableState(false);
            _regenerationButton.SetInteractableState(false);
        }

        public void Init(PlayerHealth playerHealth, InputSwitcher inputSwitcher, float delay)
        {
            _playerHealth = playerHealth;
            _inputSwitcher = inputSwitcher;
            _delay = delay;
            
            _playerHealth.Died += OnDied;
        }   

        private void OnPlayerRegenerated()
        {
            Time.timeScale = 1;
            _canvasGroup.alpha = 0;
            
            _restartButton.SetInteractableState(false);
            _regenerationButton.SetInteractableState(false);
            
            if (_inputSwitcher.InputSource is UIInput)
                _joystick.gameObject.SetActive(true);

            _pauseButton.gameObject.SetActive(true);
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
            _canvasGroup.alpha = 1;

            _restartButton.SetInteractableState(true);

            if (_regenerationButton.IsClicked == false)
                _regenerationButton.SetInteractableState(true);

            if (_inputSwitcher.InputSource is UIInput)
                _joystick.gameObject.SetActive(false);
            
            _pauseButton.gameObject.SetActive(false);

            _isCoroutineStarted = false;
        }
    }
}