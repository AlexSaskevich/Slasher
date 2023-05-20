using System;
using System.Collections.Generic;
using System.Linq;
using Source.GameLogic.BotsSpawners;
using Source.GameLogic.Scores;
using Source.GameLogic.Timers;
using Source.Player;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class SpawnerInitializer : MonoBehaviour
    {
        [SerializeField] private FirstGameModeBlinder _firstGameModeBlinder;
        [SerializeField] private List<BotsSpawner> _botsSpawners = new();
        [SerializeField] private bool _isGameModeIsTimeMode;

        public void InitSpawners(PlayerCharacter playerCharacter)
        {
            if (playerCharacter.TryGetComponent(out PlayerMovement playerMovement) == false)
                throw new ArgumentNullException();
            
            if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                throw new ArgumentNullException();
            
            if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                throw new ArgumentNullException();
            
            foreach (var botsSpawner in _botsSpawners.Where(botsSpawner => botsSpawner != null))
            {
                botsSpawner.Init(playerMovement, playerHealth, playerWallet,
                    playerCharacter.TryGetComponent(out ZombieScore zombieScore) ? zombieScore : null,
                    playerCharacter.TryGetComponent(out TimeModeScore timeModeScore) ? timeModeScore : null,
                    _isGameModeIsTimeMode, _isGameModeIsTimeMode ? _firstGameModeBlinder.FirstGameModeTimer : null);
            }
        }
    }
}