﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.GameLogic
{
    public sealed class BotsSpawnersSwitcher : MonoBehaviour
    {
        [SerializeField] private List<BotsSpawner> _spawners = new();
        [SerializeField] private int _delay;
        [SerializeField] private int _workingPeacefulCitizensSpawnersCount;
        [SerializeField] private int _workingZombieSpawnersCount;
        [SerializeField] private int _workingHostileCitizensSpawnersCount;

        private readonly List<BotsSpawner> _workingSpawners = new();
        private readonly List<BotsSpawner> _peacefulCitizensSpawners = new();
        private readonly List<BotsSpawner> _zombieSpawners = new();
        private readonly List<BotsSpawner> _hostileCitizensSpawners = new();

        private Coroutine _waitingTime;

        private void Awake()
        {
            foreach (var spawner in _spawners)
            {
                switch (spawner.SpawnerBotsStatus)
                {
                    case SpawnerBotsStatus.PeacefulCitizens:
                        _peacefulCitizensSpawners.Add(spawner);
                        break;
                    
                    case SpawnerBotsStatus.Zombie:
                        _zombieSpawners.Add(spawner);
                        break;
                    
                    case SpawnerBotsStatus.HostileCitizens:
                        _hostileCitizensSpawners.Add(spawner);
                        break;
                    
                    default:
                        throw new NullReferenceException();
                }
            }
        }

        private void OnEnable()
        {
            foreach (var spawner in _spawners)
                spawner.TurnedOff += OnTurnedOff;
        }

        private void OnDisable()
        {
            foreach (var spawner in _spawners)
                spawner.TurnedOff -= OnTurnedOff;
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
            
            SetWorkingSpawners(_peacefulCitizensSpawners, _workingPeacefulCitizensSpawnersCount);
            SetWorkingSpawners(_zombieSpawners, _workingZombieSpawnersCount);
            SetWorkingSpawners(_hostileCitizensSpawners, _workingHostileCitizensSpawnersCount);

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
            if (_workingSpawners.All(spawner => spawner.CurrentWave == null))
            {
                if (_waitingTime != null)
                    StopCoroutine(_waitingTime);

                _waitingTime = StartCoroutine(WaitTime());
                
                print("TurnedOff");
            }
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
    }
}