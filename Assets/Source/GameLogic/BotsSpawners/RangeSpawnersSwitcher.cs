using System.Linq;
using UnityEngine;

namespace Source.GameLogic.BotsSpawners
{
    public sealed class RangeSpawnersSwitcher : SpawnersSwitcher
    {
        [SerializeField] private int _workingSpawnersCount;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < Delay || GetWorkingBotsSpawnersCount() > 0)
                return;
            
            _timer = 0;
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