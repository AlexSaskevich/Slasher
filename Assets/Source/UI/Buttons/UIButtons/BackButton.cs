using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class BackButton : UIButton
    {
        [SerializeField] private GameModeView _gameModeView;

        protected override void OnUIButtonClick()
        {
            _gameModeView.Hide();
        }
    }
}