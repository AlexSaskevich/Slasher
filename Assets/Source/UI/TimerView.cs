using Source.GameLogic;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    public sealed class TimerView : MonoBehaviour
    {
        [SerializeField] private TimerListener _timerListener;
        
        private TMP_Text _time;
        private Timer _timer;
        
        private void Awake()
        {
            _timer = _timerListener.Timer;   
            _time = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _time.text = GameProgressSaver.GetTime();
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);

            _time.text = _timer.Seconds.ToString().Length == 1
                ? $"{_timer.Minutes} : 0{_timer.Seconds}"
                : $"{_timer.Minutes} : {_timer.Seconds}";
        }
    }
}