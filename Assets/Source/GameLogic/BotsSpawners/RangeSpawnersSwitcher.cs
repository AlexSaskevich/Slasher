using System.Linq;
using Source.GameLogic.Timers;
using UnityEngine;

namespace Source.GameLogic.BotsSpawners
{
    public sealed class RangeSpawnersSwitcher : SpawnersSwitcher
    {
        [SerializeField] private int _workingSpawnersCount;
        [SerializeField] private TimerListener _timerListener;
        
        private float _timer;

        protected override void OnEnable()
        {
            base.OnEnable();

            _timerListener.Initialized += OnInitialized;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _timerListener.Initialized -= OnInitialized;
            _timerListener.SecondGameModeTimer.BorderReached -= OnBorderReached;
        }

        private void OnInitialized()
        {
            _timerListener.SecondGameModeTimer.BorderReached += OnBorderReached;
        }

        private void OnBorderReached()
        {
            Activate();
        }

        private void SetSpawners()
        {
            while (_workingSpawnersCount > GetBotsSpawnersCount())
                _workingSpawnersCount--;

            if (_workingSpawnersCount == 0)
                return;
            
            for (var i = 0; i < _workingSpawnersCount; i++)
            {
                var randomSpawnerNumber = Random.Range(0, GetBotsSpawnersCount());

                while (GetWorkingBotsSpawners().Any(spawner => spawner == TryGetBotsSpawner(randomSpawnerNumber)))
                    randomSpawnerNumber = Random.Range(0, GetBotsSpawnersCount());

                AddWorkingSpawner(TryGetBotsSpawner(randomSpawnerNumber));
            }
        }

        protected override void Activate()
        {
            ResetSpawners();
            SetSpawners();

            foreach (var workingSpawner in GetWorkingBotsSpawners())
                workingSpawner.gameObject.SetActive(true);
        }
    }
}