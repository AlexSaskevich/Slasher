using System.Collections.Generic;
using Source.InputSource;
using Source.UI.Buttons.UIButtons;
using Source.UI.Views;
using Source.UI.Views.SkillViews;
using UnityEngine;

namespace Source.UI.Panels
{
    public abstract class GameScreen : MonoBehaviour
    {
        [SerializeField] private CharacterFrameView _characterFrameView;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private KeyboardInputPanel _keyboardInputPanel;
        [SerializeField] private UIInputPanel _uiInputPanel;
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
            if (_inputSwitcher.InputSource is UIInput)
                _uiInputPanel.gameObject.SetActive(state);
            else
                _keyboardInputPanel.gameObject.SetActive(state);     
            
            foreach (var skillView in _skillViews)
                skillView.gameObject.SetActive(state);
            
            _pauseButton.gameObject.SetActive(state);

            if (state)
                _characterFrameView.Show();
            else
                _characterFrameView.Hide();
        }
    }
}