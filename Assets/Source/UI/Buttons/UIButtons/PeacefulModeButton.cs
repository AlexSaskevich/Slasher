using Source.Constants;
using UnityEngine.SceneManagement;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class PeacefulModeButton : UIButton
    {
        protected override void OnUIButtonClick()
        {
            SceneManager.LoadScene(GameModeConstants.PeacefulMode);
        }
    }
}