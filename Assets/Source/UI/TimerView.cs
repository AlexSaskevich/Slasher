using Source.GameLogic;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    public sealed class TimerView : MonoBehaviour
    {
        private readonly Timer _timer = new(); 
        
        private TMP_Text _time;
        
        private void Awake()
        {
            _time = GetComponent<TMP_Text>();
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