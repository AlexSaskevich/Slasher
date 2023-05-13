using Source.Combo;
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
using Source.UI.Panels;
using Source.UI.Views;
using Source.UI.Views.SkillViews.CooldownViews;
using Source.Yandex;
using System;
using System.Collections.Generic;
using System.Linq;
using Source.UI.Views.SkillViews.DurationViews;
using UnityEngine;
using DeviceType = Agava.YandexGames.DeviceType;

namespace Source.GameLogic
{
    public sealed class PlayerCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private DeviceType _device;
        [SerializeField] private List<PlayerCharacter> _prefabs;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<ControlButton> _controlButtons;
        [SerializeField] private AdShower _adShower;
        [SerializeField] private PlayerFollower _playerFollower;
        [SerializeField] private PlayerAgilityBar _playerAgilityBar;
        [SerializeField] private PlayerHealthBar _playerHealthBar;
        [SerializeField] private PlayerManaBar _playerManaBar;
        [SerializeField] private PlayerWalletView _playerWalletView;
        [SerializeField] private BuffCooldownPCView _buffCooldownPCView;
        [SerializeField] private UltimateCooldownPCView _ultimateCooldownPCView;
        [SerializeField] private RollCooldownPCView _rollCooldownPCView;
        [SerializeField] private BuffCooldownMobileView _buffCooldownMobileView;
        [SerializeField] private UltimateCooldownMobileView _ultimateCooldownMobileView;
        [SerializeField] private RollCooldownMobileView _rollCooldownMobileView;
        [SerializeField] private BuffDurationView _buffDurationView;
        [SerializeField] private UltimateDurationView _ultimateDurationView;
        [SerializeField] private RollDurationView _rollDurationView;
        [SerializeField] private List<BotsSpawner> _botsSpawners;
        [SerializeField] private BuyCharacterButton _buyCharacterButton;
        [SerializeField] private List<BoostBlinder> _boostBlinders;
        [SerializeField] private MoneyButton _moneyButton;
        [SerializeField] private TimerBlinder _timerBlinder;
        [SerializeField] private RangeSpawnersSwitcher _rangeSpawnersSwitcher;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private ZombieScoreView _zombieScoreView;
        [SerializeField] private DeathPanel _deathPanel;
        [SerializeField] private RegenerationButton _regenerationButton;
        [SerializeField] private Vector3 _lookingPosition;
        [SerializeField] private bool _isSceneMainMenu;

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

        public void RotateCharacter(PlayerCharacter playerCharacter)
        {
            const int WRotation = 1;

            playerCharacter.transform.rotation =
                new Quaternion(_lookingPosition.x, _lookingPosition.y, _lookingPosition.z, WRotation);
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
            
            if (playerCharacter.TryGetComponent(out KeyboardInput keyboardInput) == false)
                throw new ArgumentNullException();
            
            if (_isSceneMainMenu)
            {
                keyboardInput.enabled = false;
                RotateCharacter(playerCharacter);
            }
            else
            {
                keyboardInput.enabled = true;
            }

            playerCharacter.gameObject.SetActive(true);
            GameProgressSaver.SetCurrentCharacterIndex((int)playerCharacter.PlayerCharacterName);
        }

        private void OnCharacterSet()
        {
            SetCurrentPlayer();
        }

        private void InitPlayerCharacters()
        {
            var spawnPoint = _playerSpawnPoint == null ? Vector3.zero : _playerSpawnPoint.position;

            foreach (var playerCharacter in _prefabs)
            {
                var character = Instantiate(playerCharacter, spawnPoint, Quaternion.identity, null);

                if (character.TryGetComponent(out InputSwitcher inputSwitcher) == false)
                    throw new ArgumentNullException();

                if (character.TryGetComponent(out UIInput uiInput) == false)
                    throw new ArgumentNullException();

                if (character.TryGetComponent(out PlayerHealth playerHealth) == false)
                    throw new ArgumentNullException();

                if (_joystick != null && _controlButtons != null)
                    uiInput.Init(_joystick, _controlButtons);

                inputSwitcher.Init(_device);
                //inputSwitcher.Init(Agava.YandexGames.Device.Type);

                if (_adShower != null)
                    playerHealth.Init(_adShower);
                
                _playerCharacters.Add(character);
                character.gameObject.SetActive(false);
            }
        }

        private void InitObjects(PlayerCharacter playerCharacter)
        {
            if (playerCharacter.TryGetComponent(out PlayerAgility playerAgility) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerHealth playerHealth) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerMana playerMana) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Buff buff) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Ultimate ultimate) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Roll roll) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerMovement playerMovement) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out InputSwitcher inputSwitcher) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerCombo playerCombo) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out Animator animator) == false)
                throw new ArgumentNullException();

            if (playerCharacter.TryGetComponent(out PlayerDeathAnimation playerDeathAnimation) == false)
                throw new ArgumentNullException();

            if (_playerFollower != null)
                _playerFollower.Init(playerCharacter.transform);

            if (_playerAgilityBar != null)
                _playerAgilityBar.Init(playerHealth, playerAgility);

            if (_playerHealthBar != null)
                _playerHealthBar.Init(playerHealth);

            if (_playerManaBar != null)
                _playerManaBar.Init(playerHealth, playerMana);

            if (_playerWalletView != null)
                _playerWalletView.Init(playerWallet);

            if (_buyCharacterButton != null)
                _buyCharacterButton.Init(playerWallet);

            if (_moneyButton != null)
                _moneyButton.Init(playerWallet);

            if (_timerBlinder != null && _rangeSpawnersSwitcher != null)
                _timerBlinder.Init(_rangeSpawnersSwitcher.Delay, playerHealth);

            if (_deathPanel != null)
                _deathPanel.Init(playerHealth, inputSwitcher, playerDeathAnimation.GetLenght());

            if (_regenerationButton != null)
            {
                _regenerationButton.Init(playerHealth, playerCombo, inputSwitcher.InputSource, animator, playerMana,
                    playerAgility);
            }

            if (playerCharacter.TryGetComponent(out ZombieScore score) && _zombieScoreView != null)
                _zombieScoreView.Init(score);

            if (_buffCooldownPCView != null && _buffDurationView != null)
            {
                _buffCooldownMobileView.Init(buff, inputSwitcher.InputSource);
                _buffCooldownPCView.Init(buff, inputSwitcher.InputSource);
                _buffDurationView.Init(buff, inputSwitcher.InputSource);
            }

            if (_ultimateCooldownPCView != null && _ultimateDurationView != null)
            {
                _ultimateCooldownMobileView.Init(ultimate, inputSwitcher.InputSource);
                _ultimateCooldownPCView.Init(ultimate, inputSwitcher.InputSource);
                _ultimateDurationView.Init(ultimate, inputSwitcher.InputSource);
            }

            if (_rollCooldownPCView != null && _rollDurationView != null)
            {
                _rollCooldownMobileView.Init(roll, inputSwitcher.InputSource);
                _rollCooldownPCView.Init(roll, inputSwitcher.InputSource);
                _rollDurationView.Init(roll, inputSwitcher.InputSource);
            }

            foreach (var boostBlinder in _boostBlinders)
            {
                switch (boostBlinder.GoodStatus)
                {
                    case GoodStatus.HealthUpgradeable:
                        {
                            boostBlinder.Init(playerWallet, playerHealth, playerCharacter.PlayerCharacterName);
                            break;
                        }
                    case GoodStatus.ManaUpgradeable:
                        {
                            boostBlinder.Init(playerWallet, playerMana, playerCharacter.PlayerCharacterName);
                            break;
                        }
                    case GoodStatus.AgilityUpgradeable:
                        {
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

            foreach (var botsSpawner in _botsSpawners.Where(botsSpawner => botsSpawner != null))
            {
                botsSpawner.Init(playerMovement, playerHealth, playerWallet,
                    playerCharacter.TryGetComponent(out ZombieScore zombieScore) ? zombieScore : null);
            }
        }
    }
}