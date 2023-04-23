﻿using Source.GameLogic;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class FirstGameModeTimerView : MonoBehaviour
    {
        [SerializeField] private FirstGameModeBlinder _firstGameModeBlinder;

        private TMP_Text _timer;
        
        private void Awake()
        {
            _timer = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            var timer = _firstGameModeBlinder.FirstGameModeTimer;

            _timer.text = timer.Seconds.ToString().Length == 1
                ? $"{timer.Minutes} : 0{timer.Seconds}"
                : $"{timer.Minutes} : {timer.Seconds}";
        }
    }
}