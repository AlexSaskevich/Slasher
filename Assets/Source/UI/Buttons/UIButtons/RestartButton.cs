using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class RestartButton : UIButton
    {
        protected override void OnButtonClick()
        {
            Restart();
        }

        public void SetInteractableState(bool state)
        {
            Button.interactable = state;
        }
        
        private void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}