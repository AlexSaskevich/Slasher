using Source.GameLogic.Timers;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TMP_Text), typeof(FirstGameModeBlinder))]
    public sealed class FirstGameModeTimerView : MonoBehaviour
    {
        private FirstGameModeBlinder _firstGameModeBlinder;

        private TMP_Text _timer;
        
        private void Awake()
        {
            _firstGameModeBlinder = GetComponent<FirstGameModeBlinder>();
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