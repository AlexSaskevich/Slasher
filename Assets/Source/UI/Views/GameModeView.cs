﻿using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameModeView : UIView
    {
        protected override void Awake()
        {
            base.Awake();
            Hide();
        }
    }
}