using System;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class ContinueButton : UIButton
    {
        [SerializeField] private PauseButton _pauseButton;

        public event Action Continued;
        
        protected override void OnButtonClick()
        {
            Continue();
        }

        private void Continue()
        {
            Time.timeScale = 1;
            
            _pauseButton.gameObject.SetActive(true);
            Continued?.Invoke();
        }
    }
}