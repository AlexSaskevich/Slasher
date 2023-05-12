using Source.UI.Panels;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class PauseButton : UIButton
    {
        [SerializeField] private PausePanel _pausePanel;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _pausePanel.gameObject.SetActive(false);
        }

        protected override void OnButtonClick()
        {
            Pause();
        }

        private void Pause()
        {
            Time.timeScale = 0;

            _pausePanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}