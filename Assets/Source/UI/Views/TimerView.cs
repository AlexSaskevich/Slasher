using Source.GameLogic;
using Source.GameLogic.Timers;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TimerBlinder))]
    public sealed class TimerView : MonoBehaviour
    {
        private TimerBlinder _timerBlinder;
        private TMP_Text _time;
        private SecondGameModeTimer _secondGameModeTimer;
        
        private void Awake()
        {
            _timerBlinder = GetComponent<TimerBlinder>();
            _time = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _timerBlinder.Initialized += OnInitialized;
        }

        private void OnDisable()
        {
            _timerBlinder.Initialized -= OnInitialized;
        }

        private void OnInitialized()
        {
            _secondGameModeTimer = _timerBlinder.SecondGameModeTimer;   
        }

        private void Start()
        {
            _time.text = GameProgressSaver.GetTimeText();
        }

        private void Update()
        {
            if (_secondGameModeTimer == null)
                return;
            
            _time.text = _secondGameModeTimer.Seconds.ToString().Length == 1
                ? $"{_secondGameModeTimer.Minutes} : 0{_secondGameModeTimer.Seconds}"
                : $"{_secondGameModeTimer.Minutes} : {_secondGameModeTimer.Seconds}";
        }
    }
}