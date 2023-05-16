using Source.InputSource;
using Source.UI.Buttons.UIButtons;
using Source.UI.Views;
using Source.UI.Views.SkillViews;
using System.Collections.Generic;
using UnityEngine;

namespace Source.UI.Panels
{
    public abstract class GameScreen : MonoBehaviour
    {
        [SerializeField] private CharacterFrameView _characterFrameView;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private KeyboardInputView _keyboardInputView;
        [SerializeField] private UIInputView _uiInputView;
        [SerializeField] private List<SkillView> _skillViews;

        private InputSwitcher _inputSwitcher;

        protected CanvasGroup CanvasGroup { get; private set; }

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();

            CanvasGroup.alpha = 0;
        }

        public void Init(InputSwitcher inputSwitcher)
        {
            _inputSwitcher = inputSwitcher;
        }

        protected void SetObjectsActiveState(bool state)
        {
            switch (state)
            {
                case true:
                    ShowObjects();
                    break;
                case false:
                    HideObjects();
                    break;
            }
        }

        private void ShowObjects()
        {
            if (_inputSwitcher.InputSource is UIInput)
                _uiInputView.Show();
            else
                _keyboardInputView.Show();

            foreach (var skillView in _skillViews)
                skillView.Show();

            _pauseButton.gameObject.SetActive(true);

            _characterFrameView.Show();
        }

        private void HideObjects()
        {
            if (_inputSwitcher.InputSource is UIInput)
                _uiInputView.Hide();
            else
                _keyboardInputView.Hide();

            foreach (var skillView in _skillViews)
                skillView.Hide();

            _pauseButton.gameObject.SetActive(false);

            _characterFrameView.Hide();
        }
    }
}