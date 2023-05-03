using Source.Enums;
using Source.InputSource;
using Source.Player;
using Source.UI.Bars;
using Source.UI.Buttons.ControlButtons;
using Source.UI.Buttons.UIButtons;
using Source.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Source.GameLogic.BotsSpawners;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<PlayerCharacter> _prefabs = new();
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<ControlButton> _controlButtons = new();
        [SerializeField] private PlayerFollower _playerFollower;
        [SerializeField] private PlayerAgilityBar _playerAgilityBar;
        [SerializeField] private PlayerHealthBar _playerHealthBar;
        [SerializeField] private PlayerManaBar _playerManaBar;
        [SerializeField] private PlayerWalletView _playerWalletView;
        [SerializeField] private List<BotsSpawner> _botsSpawners = new();
        [SerializeField] private BuyCharacterButton _buyCharacterButton;

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

                if (_joystick != null)
                    inputSwitcher.Init(_joystick);

                if (_joystick != null && _controlButtons.Count != 0)
                    uiInput.Init(_joystick, _controlButtons);

                _playerCharacters.Add(character);
                character.gameObject.SetActive(false);
            }
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

            playerCharacter.gameObject.SetActive(true);
            GameProgressSaver.SetCurrentCharacterIndex((int)playerCharacter.PlayerCharacterName);
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

            if (_buyCharacterButton != null)
            {
                if (playerCharacter.TryGetComponent(out PlayerWallet playerWallet) == false)
                    throw new ArgumentNullException();

                _buyCharacterButton.Init(playerWallet);
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