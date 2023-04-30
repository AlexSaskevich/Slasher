using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class BackButton : UIButton
    {
        [SerializeField] private UIView _uiViewToHide;

        protected override void OnButtonClick()
        {
            _uiViewToHide.Hide();
        }
    }
}