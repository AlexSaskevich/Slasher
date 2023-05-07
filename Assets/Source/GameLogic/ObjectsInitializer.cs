using System;
using System.Collections.Generic;
using System.Linq;
using Source.Enums;
using Source.GameLogic.Boosters;
using Source.GameLogic.BotsSpawners;
using Source.Player;
using Source.Skills;
using Source.UI.Bars;
using Source.UI.Buttons.UIButtons;
using Source.UI.Views;
using Source.UI.Views.SkillViews;
using UnityEngine;

namespace Source.GameLogic
{
    [RequireComponent(typeof(PlayerCharacterSpawner))]
    public sealed class ObjectsInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerFollower _playerFollower;
        [SerializeField] private PlayerAgilityBar _playerAgilityBar;
        [SerializeField] private PlayerHealthBar _playerHealthBar;
        [SerializeField] private PlayerManaBar _playerManaBar;
        [SerializeField] private PlayerWalletView _playerWalletView;
        [SerializeField] private BuffCooldownView _buffCooldownView;
        [SerializeField] private BuffEffectView _buffEffectView;
        [SerializeField] private UltimateCooldownView _ultimateCooldownView;
        [SerializeField] private UltimateEffectView _ultimateEffectView;
        [SerializeField] private RollCooldownView _rollCooldownView;
        [SerializeField] private RollEffectView _rollEffectView;
        [SerializeField] private List<BotsSpawner> _botsSpawners;
        [SerializeField] private BuyCharacterButton _buyCharacterButton;
        [SerializeField] private List<BoostBlinder> _boostBlinders;
        [SerializeField] private MoneyButton _moneyButton;

        private PlayerCharacterSpawner _playerCharacterSpawner;

        private void Awake()
        {
            _playerCharacterSpawner = GetComponent<PlayerCharacterSpawner>();
        }

        private void OnEnable()
        {
            if (_buyCharacterButton != null)
                _buyCharacterButton.CharacterSet += OnCharacterSet;

            _playerCharacterSpawner.PlayerSet += OnPlayerSet;
        }

        private void OnDisable()
        {
            if (_buyCharacterButton != null)
                _buyCharacterButton.CharacterSet -= OnCharacterSet;
        }

        private void OnCharacterSet()
        {
            _playerCharacterSpawner.SetCurrentPlayer();
        }

        private void OnPlayerSet(PlayerCharacter playerCharacter)
        {
            InitObjects(playerCharacter);
        }
        
        private void InitObjects(PlayerCharacter playerCharacter)
        {
            if (_playerFollower != null)
                _playerFollower.Init(playerCharacter.transform);

            if (_playerAgilityBar != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerAgility playerAgility) == false)
                    throw new ArgumentNullException();

                _playerAgilityBar.Init(playerAgility);
            }

            if (_playerHealthBar != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                    throw new ArgumentNullException();

                _playerHealthBar.Init(playerHealth);
            }

            if (_playerManaBar != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerMana playerMana) == false)
                    throw new ArgumentNullException();

                _playerManaBar.Init(playerMana);
            }

            if (_playerWalletView != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                    throw new ArgumentNullException();

                _playerWalletView.Init(playerWallet);
            }

            if (_buffCooldownView != null && _buffEffectView != null)
            {
                if (playerCharacter.TryGetComponent(out Buff buff) == false)
                    throw new ArgumentNullException();

                _buffCooldownView.Init(buff);
                _buffEffectView.Init(buff);
            }

            if (_ultimateCooldownView != null && _ultimateEffectView != null)
            {
                if (playerCharacter.TryGetComponent(out Ultimate ultimate) == false)
                    throw new ArgumentNullException();

                _ultimateCooldownView.Init(ultimate);
                _ultimateEffectView.Init(ultimate);
            }

            if (_rollCooldownView != null && _rollEffectView != null)
            {
                if (playerCharacter.TryGetComponent(out Roll roll) == false)
                    throw new ArgumentNullException();

                _rollCooldownView.Init(roll);
                _rollEffectView.Init(roll);
            }

            if (_buyCharacterButton != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                    throw new ArgumentNullException();

                _buyCharacterButton.Init(playerWallet);
            }

            if (_moneyButton != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                    throw new ArgumentNullException();

                _moneyButton.Init(playerWallet);
            }
            
            foreach (var boostBlinder in _boostBlinders)
            {
                if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                    throw new ArgumentNullException();

                switch (boostBlinder.GoodStatus)
                {
                    case GoodStatus.HealthUpgradeable:
                    {
                        if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                            throw new ArgumentNullException();

                        boostBlinder.Init(playerWallet, playerHealth, playerCharacter.PlayerCharacterName);
                        break;
                    }
                    case GoodStatus.ManaUpgradeable:
                    {
                        if (playerCharacter.TryGetComponent(out PlayerMana playerMana) == false)
                            throw new ArgumentNullException();

                        boostBlinder.Init(playerWallet, playerMana, playerCharacter.PlayerCharacterName);
                        break;
                    }
                    case GoodStatus.AgilityUpgradeable:
                    {
                        if (playerCharacter.TryGetComponent(out PlayerAgility playerAgility) == false)
                            throw new ArgumentNullException();

                        boostBlinder.Init(playerWallet, playerAgility, playerCharacter.PlayerCharacterName);
                        break;
                    }
                    default:
                        throw new ArgumentNullException();
                }
            }

            if (_botsSpawners.Count == 0)
                return;

            foreach (var botsSpawner in _botsSpawners.Where(botsSpawner => botsSpawner != null))
            {
                if (playerCharacter.TryGetComponent(out PlayerMovement playerMovement) == false)
                    throw new ArgumentNullException();

                if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                    throw new ArgumentNullException();

                if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                    throw new ArgumentNullException();

                botsSpawner.Init(playerMovement, playerHealth, playerWallet,
                    playerCharacter.TryGetComponent(out ZombieScore zombieScore) ? zombieScore : null);
            }
        }
    }
}