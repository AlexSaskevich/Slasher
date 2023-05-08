using Source.Enums;
using Source.InputSource;
using Source.Player;
using Source.UI.Buttons.ControlButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using Source.Yandex;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<PlayerCharacter> _prefabs;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<ControlButton> _controlButtons;
        [SerializeField] private AdShower _adShower;

        private readonly List<PlayerCharacter> _playerCharacters = new();

        private PlayerCharacter _currentCharacter;

        public event Action<PlayerCharacter> PlayerSet;

        private void Start()
        {
            GameProgressSaver.SetCharacterBoughtStatus(PlayerCharacterName.Biker, true);

            InitPlayerCharacters();
            SetCurrentPlayer();
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
            
            PlayerSet?.Invoke(playerCharacter);

            playerCharacter.gameObject.SetActive(true);
            GameProgressSaver.SetCurrentCharacterIndex((int)playerCharacter.PlayerCharacterName);
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
    }
}