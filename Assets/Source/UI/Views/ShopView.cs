using Source.UI.Views;
using UnityEngine;

namespace Assets.Source.UI.Views
{
    [RequireComponent(typeof(CanvasGroup))]

    public class ShopView : UIView
    {
        protected override void Awake()
        {
            base.Awake();
            Hide();
        }
    }
}