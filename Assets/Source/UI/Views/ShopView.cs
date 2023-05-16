using UnityEngine;

namespace Source.UI.Views
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