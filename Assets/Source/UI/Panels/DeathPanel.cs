using System.Collections;
using System.Collections.Generic;
using Source.InputSource;
using Source.Player;
using Source.UI.Buttons.UIButtons;
using Source.UI.Views.SkillViews;
using UnityEngine;

namespace Source.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class DeathPanel : MonoBehaviour
    {
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private RestartButton _restartButton;
        [SerializeField] private RegenerationButton _regenerationButton;
        [SerializeField] private ExitButton _exitButton;
        [SerializeField] private KeyboardInputPanel _keyboardInputPanel;
        [SerializeField] private UIInputPanel _uiInputPanel;
        [SerializeField] private List<SkillView> _skillViews;

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
            _exitButton.transform.SetParent(transform);

            _canvasGroup.interactable = false;
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

            _canvasGroup.interactable = false;

            if (_inputSwitcher.InputSource is UIInput)
                _uiInputPanel.gameObject.SetActive(true);
            else
                _keyboardInputPanel.gameObject.SetActive(true);     
            
            foreach (var skillView in _skillViews)
                skillView.gameObject.SetActive(true);

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

            _canvasGroup.interactable = true;

            _regenerationButton.SetInteractableState(_regenerationButton.IsClicked == false);

            if (_inputSwitcher.InputSource is UIInput)
                _uiInputPanel.gameObject.SetActive(false);
            else
                _keyboardInputPanel.gameObject.SetActive(false);
            
            foreach (var skillView in _skillViews)
                skillView.gameObject.SetActive(false);

            _pauseButton.gameObject.SetActive(false);

            _isCoroutineStarted = false;
        }
    }
}