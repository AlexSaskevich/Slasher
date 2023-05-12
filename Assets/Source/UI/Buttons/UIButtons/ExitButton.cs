using Source.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class ExitButton : UIButton
    {
        protected override void OnButtonClick()
        {
            Exit();
        }

        private void Exit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(ScenesNames.MainMenu);
        }
    }
}