using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Bot;
using Source.Enums;
using Source.GameLogic.Scores;
using Source.GameLogic.Timers;
using Source.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.GameLogic.BotsSpawners
{
    public sealed class BotsSpawner : BotsPool
    {
        [SerializeField] private List<Wave> _waves = new();
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private List<Target> _targets;
        [SerializeField] private float _delay;
        
        private PlayerMovement _playerMovement;
        private PlayerHealth _playerHealth;
        private PlayerWallet _playerWallet;
        private ZombieScore _zombieScore;
        private int _currentWaveNumber;
        private int _spawnedBotsCount;
        private float _timeAfterLastBotSpawned;
        private Coroutine _coroutine;
        private TimeModeScore _timeModeScore;
        private FirstGameModeTimer _firstGameModeTimer;
        private bool _isGameModeTimeMode;
        
        public event Action TurnedOff;

        [field: SerializeField] public BotStatus BotStatus { get; private set; }
        
        public Wave CurrentWave { get; private set; }

        private void Start()
        {
            ResetOptions();

            for (var i = 0; i < CurrentWave.BotsCount; i++)
            {
                var index = Random.Range(0, CurrentWave.GetBotsMovementsLength());
                var botMovement = CurrentWave.TryGetBotMovement(index);
                
                if (botMovement == null)
                    throw new ArgumentNullException();

                Init(botMovement, transform.position);
            }
        }

        private void Update()
        {
            if (CurrentWave == null)
                return;

            _timeAfterLastBotSpawned += Time.deltaTime;
            
            if (_timeAfterLastBotSpawned >= CurrentWave.Delay)
            {
                if (TryGetBot(out var botMovement) == false)
                    return;

                _timeAfterLastBotSpawned = 0;
                
                SetBot(botMovement);
                _spawnedBotsCount++;
            }

            if (_spawnedBotsCount < CurrentWave.BotsCount)
                return;
            
            CurrentWave = null;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitTimeBetweenWaves());

            if (_currentWaveNumber != _waves.Count - 1)
                return;
            
            CurrentWave = null;
            TurnedOff?.Invoke();
        }

        public void Init(PlayerMovement playerMovement, PlayerHealth playerHealth, PlayerWallet playerWallet,
            ZombieScore zombieScore, TimeModeScore timeModeScore, bool isGameModeIsTimeMode,
            FirstGameModeTimer firstGameModeTimer)
        {
            _playerMovement = playerMovement;
            _playerHealth = playerHealth;
            _playerWallet = playerWallet;
            _zombieScore = zombieScore;
            _timeModeScore = timeModeScore;
            _isGameModeTimeMode = isGameModeIsTimeMode;
            _firstGameModeTimer = firstGameModeTimer;
        }
        
        public void ResetOptions()
        {
            _spawnedBotsCount = 0;
            _currentWaveNumber = 0;
            
            TrySetWave(_currentWaveNumber);
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
                var availableTargets =
                    _targets.Where(target => target.Status == botMovement.BotStatus && target.IsAvailable).ToArray();

                if (availableTargets.Length == 0)
                    throw new ArgumentNullException();
                
                var target = availableTargets[Random.Range(0, availableTargets.Length)];
                target.SetAvailableStatus(false);
                
                botTargets.Add(target);
                _targets.Remove(target);
            }

            botTarget.Init(_playerMovement, botTargets.ToArray());
            
            _targets.AddRange(botTargets);
            
            if(botMovement.TryGetComponent(out BotAttacker botAttacker))
                botAttacker.Init(_playerHealth);

            if (botMovement.TryGetComponent(out BotEscaper botEscaper))
                botEscaper.Init(_playerMovement.transform);

            if (botMovement.TryGetComponent(out BotHealth botHealth) == false)
                throw new ArgumentNullException();
            
            botHealth.ResetHealth();

            if (botMovement.TryGetComponent(out BotZombieHealth botZombieHealth))
                botZombieHealth.Init(_zombieScore);

            if (botMovement.TryGetComponent(out BotHumanHealth botHumanHealth))
                botHumanHealth.Init(_timeModeScore, _isGameModeTimeMode, _firstGameModeTimer);            

            if (botMovement.TryGetComponent(out BotReward botReward) == false)
                throw new ArgumentNullException();

            botReward.Init(_playerWallet);
        }
        
        private void TrySetWave(int waveNumber)
        {
            if (waveNumber < 0 || waveNumber >= _waves.Count)
                return;

            CurrentWave = _waves[waveNumber];
        }

        private IEnumerator WaitTimeBetweenWaves()
        {
            yield return new WaitForSeconds(_delay);
            
            _spawnedBotsCount = 0;
            _currentWaveNumber++;
            
            TrySetWave(_currentWaveNumber);
        }
    }
}