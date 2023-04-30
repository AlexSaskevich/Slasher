using Assets.Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public class ShopButton : UIButton
    {
        [SerializeField] private ShopView _shopView;

        protected override void OnButtonClick()
        {
            _shopView.Show();
        }
    }
}