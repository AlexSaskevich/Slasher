using Source.Constants;
using UnityEngine.SceneManagement;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class TimeModeButton : UIButton
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(ScenesNames.TimeMode);
        }
    }
}