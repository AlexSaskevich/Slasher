using System;
using Source.GameLogic.Scores;
using Source.UI.Panels;
using UnityEngine;

namespace Source.GameLogic.Timers
{
    public sealed class FirstGameModeBlinder : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private EndScreen _endScreen;
        
        private TimeModeScore _timeModeScore;

        public event Action Initialized;
        
        public FirstGameModeTimer FirstGameModeTimer { get; private set; }

        private void OnDisable()
        {
            FirstGameModeTimer.Ended -= OnEnded;
        }

        private void Start()
        {
            FirstGameModeTimer = new FirstGameModeTimer(_time);
            Initialized?.Invoke();
            
            FirstGameModeTimer.Ended += OnEnded;
        }

        private void Update()
        {
            FirstGameModeTimer.Update(Time.deltaTime);
        }

        public void Init(TimeModeScore timeModeScore)
        {
            _timeModeScore = timeModeScore;
        }
        
        private void OnEnded()
        {
            _timeModeScore.TrySetScore();
            _endScreen.End();
        }
    }
}