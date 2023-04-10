﻿using System;
using System.Collections;
using System.Collections.Generic;
using Source.Bot;
using Source.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.GameLogic
{
    public sealed class BotsSpawner : BotsPool
    {
        [SerializeField] private List<Wave> _waves = new();
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private List<Target> _targets;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private float _delay;

        private int _currentWaveNumber;
        private int _spawnedBotsCount;
        private float _timeAfterLastBotSpawned;

        public event Action TurnedOff;

        [field: SerializeField] public SpawnerBotsStatus SpawnerBotsStatus;
        
        public Wave CurrentWave { get; private set; }
        
        private void Start()
        {
            ResetOptions();

            for (var i = 0; i < CurrentWave.BotsCount; i++)
                Init(CurrentWave.BotMovements[Random.Range(0, CurrentWave.BotMovements.Length)], transform.position);
        }

        private void Update()
        {
            if(CurrentWave == null)
                return;

            _timeAfterLastBotSpawned += Time.deltaTime;

            if (_timeAfterLastBotSpawned >= CurrentWave.Delay)
            {
                if (TryGetBot(out var botMovement) == false)
                    return;
                
                SetBot(botMovement);
                _spawnedBotsCount++;
                _timeAfterLastBotSpawned = 0;
            }

            if (_spawnedBotsCount < CurrentWave.BotsCount)
                return;
            
            CurrentWave = null;
            StartCoroutine(WaitTimeBetweenWaves());

            if (_currentWaveNumber == _waves.Count - 1)
            {
                CurrentWave = null;
                TurnedOff?.Invoke();
            }
        }
        
        public void ResetOptions()
        {
            _spawnedBotsCount = 0;
            _currentWaveNumber = 0;
            
            SetWave(_currentWaveNumber);
        }

        private void SetBot(BotMovement botMovement)
        {
            var spawnPointNumber = Random.Range(0, _spawnPoints.Length);

            botMovement.transform.position = _spawnPoints[spawnPointNumber].position;
            botMovement.gameObject.SetActive(true);

            if (botMovement.TryGetComponent(out BotTarget botTarget) == false)
                throw new ArgumentNullException();

            var botTargets = new List<Target>();
            
            for (var i = 0; i < botTarget.TargetsCount; i++)
            {
                var target = _targets[Random.Range(0, _targets.Count)];
                    
                botTargets.Add(target);
                _targets.Remove(target);
            }

            botTarget.Init(_playerMovement, botTargets.ToArray());
            
            _targets.AddRange(botTargets);
            
            if(botMovement.TryGetComponent(out BotAttacker botAttacker) == false)
                return;
            
            botAttacker.Init(_playerHealth);
        }
        
        private void SetWave(int waveNumber)
        {
            if (waveNumber >= 0 && waveNumber < _waves.Count)
                CurrentWave = _waves[waveNumber];
        }

        private IEnumerator WaitTimeBetweenWaves()
        {
            yield return new WaitForSeconds(_delay);

            _spawnedBotsCount = 0;
            _currentWaveNumber++;
            
            SetWave(_currentWaveNumber);
        }
    }

    [Serializable]
    public sealed class Wave
    {
        [field: SerializeField] public BotMovement[] BotMovements { get; private set; }
        [field: SerializeField] public int BotsCount { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
    } 
}