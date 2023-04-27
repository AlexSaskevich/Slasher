using System;
using System.Collections.Generic;
using System.Linq;
using Source.Enums;
using Source.GameLogic.BotsSpawnersSystem;
using Source.InputSource;
using Source.Player;
using Source.UI.Bars;
using Source.UI.Buttons.ControlButtons;
using Source.UI.Views;
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
        [SerializeField] private List<BotsSpawner> _botsSpawners;

        private readonly List<PlayerCharacter> _playerCharacters = new();

        private void Awake()
        {
            InitPlayerCharacters();
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
            var currentCharacterName = GameProgressSaver.GetCurrentCharacterName();

            if (string.IsNullOrEmpty(currentCharacterName))
                currentCharacterName = PlayerCharacterName.Biker.ToString();

            var playerCharacter = _playerCharacters.FirstOrDefault(character =>
                character.PlayerCharacterName.ToString() == currentCharacterName);

            if (playerCharacter == null)
                throw new ArgumentNullException();

            InitObjects(playerCharacter);

            playerCharacter.gameObject.SetActive(true);
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

            if (_playerHealthBar != null)
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
            
            foreach (var botsSpawner in _botsSpawners)
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