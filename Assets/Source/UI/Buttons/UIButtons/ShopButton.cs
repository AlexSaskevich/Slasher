﻿using Cinemachine;
using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public class ShopButton : UIButton
    {
        private const int ShopCameraPriority = 1;

        [SerializeField] private ShopView _shopView;
        [SerializeField] private MenuView _menuView;
        [SerializeField] private CinemachineVirtualCamera _shopVirtualCamera;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();

            _shopVirtualCamera.Priority = ShopCameraPriority;

            _menuView.Hide();
            _shopView.Show();
        }
    }
}