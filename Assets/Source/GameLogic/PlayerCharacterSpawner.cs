using Source.Enums;
using Source.GameLogic.Scores;
using Source.GameLogic.Timers;
using Source.InputSource;
using Source.Player;
using Source.UI.Buttons.ControlButtons;
using Source.UI.Buttons.UIButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using Source.UI;
using UnityEngine;
using DeviceType = Agava.YandexGames.DeviceType;

namespace Source.GameLogic
{
    public sealed class PlayerCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectsInitializer _objectsInitializer;
        [SerializeField] private SpawnerInitializer _spawnerInitializer;
        [SerializeField] private DeviceType _device;
        [SerializeField] private List<PlayerCharacter> _prefabs;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<ControlButton> _controlButtons;
        [SerializeField] private BuyCharacterButton _buyCharacterButton;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private FirstGameModeBlinder _firstGameModeBlinder;
        [SerializeField] private Vector3 _lookingPosition;
        [SerializeField] private bool _isSceneMainMenu;
        [SerializeField] private List<Tutorial> _tutorials;

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

            if (_firstGameModeBlinder != null)
                _firstGameModeBlinder.Initialized += OnInitialized;
        }

        private void OnDisable()
        {
            if (_buyCharacterButton != null)
                _buyCharacterButton.CharacterSet -= OnCharacterSet;
            
            if (_firstGameModeBlinder != null)
                _firstGameModeBlinder.Initialized -= OnInitialized;
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

        public void SetCurrentPlayer()
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

            _objectsInitializer.InitObjects(playerCharacter);
            
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

        private void OnInitialized()
        {
            _spawnerInitializer.InitSpawners(_currentCharacter);
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

                if (character.TryGetComponent(out TimeModeScore timeModeScore) == false)
                    throw new ArgumentNullException();

                if (_joystick != null && _controlButtons != null)
                    uiInput.Init(_joystick, _controlButtons);

                if (_firstGameModeBlinder != null)
                    _firstGameModeBlinder.Init(timeModeScore);

                //inputSwitcher.Init(_device);
                inputSwitcher.Init(Agava.YandexGames.Device.Type);

                foreach (var tutorial in _tutorials)
                    tutorial.gameObject.SetActive(false);
                
                if (inputSwitcher.InputSource is UIInput)
                    _tutorials.FirstOrDefault(tutorial => tutorial is UITutorial)?.gameObject.SetActive(true);
                else
                    _tutorials.FirstOrDefault(tutorial => tutorial is KeyboardTutorial)?.gameObject.SetActive(true);

                _playerCharacters.Add(character);
                character.gameObject.SetActive(false);
            }
        }
    }
}