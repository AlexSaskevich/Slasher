using Source.Enums;
using Source.GameLogic.Boosters;
using Source.GameLogic.BotsSpawners;
using Source.GameLogic.Timers;
using Source.InputSource;
using Source.Player;
using Source.Skills;
using Source.UI.Bars;
using Source.UI.Buttons.ControlButtons;
using Source.UI.Buttons.UIButtons;
using Source.UI.Views;
using Source.UI.Views.SkillViews;
using Source.Yandex;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<PlayerCharacter> _prefabs;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<ControlButton> _controlButtons;
        [SerializeField] private AdShower _adShower;
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
        [SerializeField] private TimerBlinder _timerBlinder;
        [SerializeField] private RangeSpawnersSwitcher _rangeSpawnersSwitcher;

        private readonly List<PlayerCharacter> _playerCharacters = new();

        private PlayerCharacter _currentCharacter;

        private void Awake()
        {
            GameProgressSaver.SetCharacterBoughtStatus(PlayerCharacterName.Biker, true);
            
            InitPlayerCharacters();
            SetCurrentPlayer();
        }

        private void OnEnable()
        {
            if (_buyCharacterButton != null)
                _buyCharacterButton.CharacterSet += OnCharacterSet;
        }

        private void OnDisable()
        {
            if (_buyCharacterButton != null)
                _buyCharacterButton.CharacterSet -= OnCharacterSet;
        }

        public IEnumerable<PlayerCharacter> GetPlayerCharacters()
        {
            return _playerCharacters;
        }

        public PlayerCharacter TryGetPlayerCharacterByIndex(int index)
        {
            if (index < 0 || index >= _playerCharacters.Count)
                return null;

            return _playerCharacters[index];
        }

        private void SetCurrentPlayer()
        {
            if (_currentCharacter != null)
                _currentCharacter.gameObject.SetActive(false);

            var currentCharacterName = GameProgressSaver.GetCurrentCharacterIndex();

            var playerCharacter =
                _playerCharacters.FirstOrDefault(
                    character => (int)character.PlayerCharacterName == currentCharacterName);

            if (playerCharacter == null)
                throw new ArgumentNullException();

            _currentCharacter = playerCharacter;

            InitObjects(playerCharacter);

            if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                throw new ArgumentNullException();
            
            playerWallet.Init();
            
            playerCharacter.gameObject.SetActive(true);
            GameProgressSaver.SetCurrentCharacterIndex((int)playerCharacter.PlayerCharacterName);
        }

        private void OnCharacterSet()
        {
            SetCurrentPlayer();
        }

        private void InitPlayerCharacters()
        {
            foreach (var playerCharacter in _prefabs)
            {
                var character = Instantiate(playerCharacter, Vector3.zero, Quaternion.identity, null);

                if (character.TryGetComponent(out InputSwitcher inputSwitcher) == false)
                    throw new ArgumentNullException();

                if (character.TryGetComponent(out UIInput uiInput) == false)
                    throw new ArgumentNullException();

                if (character.TryGetComponent(out PlayerHealth playerHealth) == false)
                    throw new ArgumentNullException();

                if (_joystick != null)
                    inputSwitcher.Init(_joystick);

                if (_joystick != null && _controlButtons.Count != 0)
                    uiInput.Init(_joystick, _controlButtons);

                if (_adShower != null)
                    playerHealth.Init(_adShower);

                _playerCharacters.Add(character);
                character.gameObject.SetActive(false);
            }
        }

        private void InitObjects(PlayerCharacter playerCharacter)
        {
            if (_playerFollower != null)
                _playerFollower.Init(playerCharacter.transform);

            if (_playerAgilityBar != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerAgility playerAgility) == false)
                    throw new ArgumentNullException();

                if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                    throw new ArgumentNullException();

                _playerAgilityBar.Init(playerHealth, playerAgility);
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

                if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                    throw new ArgumentNullException();

                _playerManaBar.Init(playerHealth, playerMana);
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

            if (_timerBlinder != null && _rangeSpawnersSwitcher != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                    throw new ArgumentNullException();

                _timerBlinder.Init(_rangeSpawnersSwitcher.Delay, playerHealth);
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

                if (boostBlinder.TryGetComponent(out BoostView boostView) == false)
                    throw new ArgumentNullException();

                boostView.Init(playerWallet);
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