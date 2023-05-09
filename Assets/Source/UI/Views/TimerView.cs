using Source.GameLogic;
using Source.GameLogic.Timers;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    [RequireComponent(typeof(TimerListener))]
    public sealed class TimerView : MonoBehaviour
    {
        private TimerListener _timerListener;
        private TMP_Text _time;
        private SecondGameModeTimer _secondGameModeTimer;
        
        private void Awake()
        {
            _timerListener = GetComponent<TimerListener>();
            _time = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _timerListener.Initialized += OnInitialized;
        }

        private void OnDisable()
        {
            _timerListener.Initialized -= OnInitialized;
        }

        private void OnInitialized()
        {
            _secondGameModeTimer = _timerListener.SecondGameModeTimer;   
        }

        private void Start()
        {
            _time.text = GameProgressSaver.GetTime();
        }

        private void Update()
        {
            if (_secondGameModeTimer == null)
                return;
            
            _secondGameModeTimer.Update(Time.deltaTime);

            _time.text = _secondGameModeTimer.Seconds.ToString().Length == 1
                ? $"{_secondGameModeTimer.Minutes} : 0{_secondGameModeTimer.Seconds}"
                : $"{_secondGameModeTimer.Minutes} : {_secondGameModeTimer.Seconds}";
        }
    }
}