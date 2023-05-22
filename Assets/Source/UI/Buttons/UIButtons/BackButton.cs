using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class BackButton : UIButton
    {
        [SerializeField] private UIView _uiViewToHide;
        [SerializeField] private UIView _uiViewToShow;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            
            _uiViewToHide.Hide();
            _uiViewToShow.Show();
        }
    }
}