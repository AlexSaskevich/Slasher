using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.GameLogic.SpawnersSystem
{
    public sealed class RangeSpawnersSwitcher : SpawnersSwitcher
    {
        [SerializeField] private int _workingSpawnersCount;

        private readonly List<BotsSpawner> _workingSpawners = new();

        private Coroutine _coroutine;
        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < Delay || _workingSpawners.Count > 0)
                return;
            
            Activate();
            _timer = 0;
        }

        private void SetSpawners()
        {
            while (_workingSpawnersCount > GetBotsSpawners().Count)
                _workingSpawnersCount--;

            if (_workingSpawnersCount == 0)
                return;
            
            for (var i = 0; i < _workingSpawnersCount; i++)
            {
                var randomSpawnerNumber = Random.Range(0, GetBotsSpawners().Count);

                while (_workingSpawners.Any(spawner => spawner == GetBotsSpawners()[randomSpawnerNumber]))
                    randomSpawnerNumber = Random.Range(0, GetBotsSpawners().Count);

                _workingSpawners.Add(GetBotsSpawners()[randomSpawnerNumber]);
            }
        }

        protected override void Activate()
        {
            ResetSpawners();
            SetSpawners();

            foreach (var workingSpawner in _workingSpawners)
                workingSpawner.gameObject.SetActive(true);
        }
    }
}