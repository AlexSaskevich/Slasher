using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class PlayButton : UIButton
    {
        [SerializeField] private GameModeView _gameModeView;
        [SerializeField] private MenuView _menuView;

        protected override void OnButtonClick()
        {
            _menuView.Hide();
            _gameModeView.Show();
        }
    }
}