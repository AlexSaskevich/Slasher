using Source.GameLogic;
using Source.GameLogic.Timers;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    public sealed class TimerView : MonoBehaviour
    {
        [SerializeField] private TimerListener _timerListener;
        
        private TMP_Text _time;
        private SecondGameModeTimer _secondGameModeTimer;
        
        private void Awake()
        {
            _secondGameModeTimer = _timerListener.SecondGameModeTimer;   
            _time = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _time.text = GameProgressSaver.GetTime();
        }

        private void Update()
        {
            _secondGameModeTimer.Update(Time.deltaTime);

            _time.text = _secondGameModeTimer.Seconds.ToString().Length == 1
                ? $"{_secondGameModeTimer.Minutes} : 0{_secondGameModeTimer.Seconds}"
                : $"{_secondGameModeTimer.Minutes} : {_secondGameModeTimer.Seconds}";
        }
    }
}