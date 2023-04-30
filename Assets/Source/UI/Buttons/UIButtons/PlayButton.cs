using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class PlayButton : UIButton
    {
        [SerializeField] private GameModeView _gameModeView;

        protected override void OnButtonClick()
        {
            _gameModeView.Show();
        }
    }
}