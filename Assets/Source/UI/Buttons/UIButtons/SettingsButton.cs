using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class SettingsButton : UIButton
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private SettingsView _settingsView;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            
            _menuView.Hide();
            _settingsView.Show();
        }
    }
}