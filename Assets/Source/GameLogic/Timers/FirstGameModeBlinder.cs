using UnityEngine;

namespace Source.GameLogic.Timers
{
    public sealed class FirstGameModeBlinder : MonoBehaviour
    {
        [SerializeField] private float _time;

        public FirstGameModeTimer FirstGameModeTimer { get; private set; }

        private void Awake()
        {
            FirstGameModeTimer = new FirstGameModeTimer(_time);
        }

        private void OnEnable()
        {
            FirstGameModeTimer.Ended += OnEnded;
        }

        private void OnDisable()
        {
            FirstGameModeTimer.Ended -= OnEnded;
        }

        private void Update()
        {
            FirstGameModeTimer.Update(Time.deltaTime);
        }

        private void OnEnded()
        {
            Time.timeScale = 0;
        }
    }
}