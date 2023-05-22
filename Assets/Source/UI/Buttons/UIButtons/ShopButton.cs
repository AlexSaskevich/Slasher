using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public class ShopButton : UIButton
    {
        [SerializeField] private ShopView _shopView;
        [SerializeField] private MenuView _menuView;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            
            _menuView.Hide();
            _shopView.Show();
        }
    }
}