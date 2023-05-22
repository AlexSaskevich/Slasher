using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class RestartButton : UIButton
    {
        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            
            Restart();
        }

        private void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}