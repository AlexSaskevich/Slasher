using System.Linq;
using Source.GameLogic.Timers;
using UnityEngine;

namespace Source.GameLogic.BotsSpawners
{
    public sealed class RangeSpawnersSwitcher : SpawnersSwitcher
    {
        [SerializeField] private int _workingSpawnersCount;
        [SerializeField] private TimerBlinder _timerBlinder;
        
        private float _timer;

        protected override void OnEnable()
        {
            base.OnEnable();

            _timerBlinder.Initialized += OnInitialized;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _timerBlinder.Initialized -= OnInitialized;
            _timerBlinder.SecondGameModeTimer.BorderReached -= OnBorderReached;
        }

        private void OnInitialized()
        {
            _timerBlinder.SecondGameModeTimer.BorderReached += OnBorderReached;
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