using System.Collections.Generic;
using System.Linq;
using Source.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.GameLogic.BotsSpawnersSystem
{
    public sealed class BotsSpawnersSwitcher : SpawnersSwitcher
    {
        [SerializeField] private int _workingPeacefulSpawnersCount;
        [SerializeField] private int _workingZombieSpawnersCount;
        [SerializeField] private int _workingHostileSpawnersCount;

        private readonly List<BotsSpawner> _peacefulSpawners = new();
        private readonly List<BotsSpawner> _zombieSpawners = new();
        private readonly List<BotsSpawner> _hostileSpawners = new();

        private Coroutine _waitingTime;

        private void Awake()
        {
            foreach (var spawner in GetBotsSpawners())
            {
                switch (spawner.BotStatus)
                {
                    case BotStatus.Peaceful:
                        _peacefulSpawners.Add(spawner);
                        break;
                    case BotStatus.Zombie:
                        _zombieSpawners.Add(spawner);
                        break;
                    case BotStatus.Hostile:
                        _hostileSpawners.Add(spawner);
                        break;
                }
            }
        }

        private void Start()
        {
            Activate();
        }

        private void SetWorkingSpawners(List<BotsSpawner> spawners, int spawnersCount)
        {
            while (IsWorkingSpawnersCountMore(spawners, spawnersCount))
                spawnersCount--;
            
            if(spawnersCount == 0)
                return;

            foreach (var spawner in spawners)
                spawner.gameObject.SetActive(false);

            for (var i = 0; i < spawnersCount; i++)
            {
                var randomSpawnerNumber = Random.Range(0, spawners.Count);

                while (GetWorkingBotsSpawners().Any(spawner => spawner == spawners[randomSpawnerNumber]))
                    randomSpawnerNumber = Random.Range(0, spawners.Count);

                GetWorkingBotsSpawners().Add(spawners[randomSpawnerNumber]);
            }
        }

        protected override void Activate()
        {
            ResetSpawners();

            SetWorkingSpawners(_peacefulSpawners, _workingPeacefulSpawnersCount);
            SetWorkingSpawners(_zombieSpawners, _workingZombieSpawnersCount);
            SetWorkingSpawners(_hostileSpawners, _workingHostileSpawnersCount);
            
            foreach (var workingSpawner in GetWorkingBotsSpawners())
                workingSpawner.gameObject.SetActive(true);
        }

        private bool IsWorkingSpawnersCountMore(ICollection<BotsSpawner> spawners, int spawnersCount)
        {
            return spawnersCount > spawners.Count;
        }
    }
}