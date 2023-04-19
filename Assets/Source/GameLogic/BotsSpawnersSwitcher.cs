using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.GameLogic
{
    public sealed class BotsSpawnersSwitcher : MonoBehaviour
    {
        [SerializeField] private List<BotsSpawner> _spawners = new();
        [SerializeField] private int _delay;
        [SerializeField] private int _workingPeacefulSpawnersCount;
        [SerializeField] private int _workingZombieSpawnersCount;
        [SerializeField] private int _workingHostileSpawnersCount;
        [SerializeField] private int _workingRangeSpawnersCount;
        [SerializeField] private RangeSpawnersActivator _rangeSpawnersActivator;

        private readonly List<BotsSpawner> _workingSpawners = new();
        private readonly List<BotsSpawner> _peacefulSpawners = new();
        private readonly List<BotsSpawner> _zombieSpawners = new();
        private readonly List<BotsSpawner> _hostileSpawners = new();
        private readonly List<BotsSpawner> _rangeSpawners = new();

        private Coroutine _waitingTime;
        private bool _canSpawnRangeEnemies;

        private void Awake()
        {
            foreach (var spawner in _spawners)
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
                    
                    case BotStatus.Range:
                        _rangeSpawners.Add(spawner);
                        break;
                    
                    default:
                        throw new ArgumentNullException();
                }
            }
        }

        private void OnEnable()
        {
            foreach (var spawner in _spawners)
                spawner.TurnedOff += OnTurnedOff;

            _rangeSpawnersActivator.Activated += OnActivated;
        }

        private void OnDisable()
        {
            foreach (var spawner in _spawners)
                spawner.TurnedOff -= OnTurnedOff;

            _rangeSpawnersActivator.Activated -= OnActivated;
        }

        private void Start()
        {
            ActivateWorkingSpawners();
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

                while (_workingSpawners.Any(spawner => spawner == spawners[randomSpawnerNumber]))
                    randomSpawnerNumber = Random.Range(0, spawners.Count);
                
                _workingSpawners.Add(spawners[randomSpawnerNumber]);
            }
        }

        private void ActivateWorkingSpawners()
        {
            ResetSpawners();
            
            SetWorkingSpawners(_peacefulSpawners, _workingPeacefulSpawnersCount);
            SetWorkingSpawners(_zombieSpawners, _workingZombieSpawnersCount);
            SetWorkingSpawners(_hostileSpawners, _workingHostileSpawnersCount);

            if (_canSpawnRangeEnemies)
                SetWorkingSpawners(_rangeSpawners, _workingRangeSpawnersCount);
            
            foreach (var workingSpawner in _workingSpawners)
                workingSpawner.gameObject.SetActive(true);
        }

        private void ResetSpawners()
        {
            foreach (var spawner in _workingSpawners)
                spawner.ResetOptions();
            
            _workingSpawners.Clear();
        }
        
        private void OnTurnedOff()
        {
            if (_workingSpawners.All(spawner => spawner.CurrentWave == null) == false)
                return;
            
            if (_waitingTime != null)
                StopCoroutine(_waitingTime);

            _waitingTime = StartCoroutine(WaitTime());
        }

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(_delay);
            
            ActivateWorkingSpawners();
        }

        private bool IsWorkingSpawnersCountMore(ICollection<BotsSpawner> spawners, int spawnersCount)
        {
            return spawnersCount > spawners.Count;
        }

        private void OnActivated(bool isActivating)
        {
            print("ActiVATE");
            _canSpawnRangeEnemies = isActivating;
        }
    }
}