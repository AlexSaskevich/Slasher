using System;
using System.Collections.Generic;
using System.Linq;
using Source.Enums;
using Source.InputSource;
using Source.Player;
using Source.UI.Buttons.ControlButtons;
using UnityEngine;

namespace Source.GameLogic
{
    public sealed class PlayerCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private List<PlayerCharacter> _prefabs = new();
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<ControlButton> _controlButtons = new();

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

            playerCharacter.gameObject.SetActive(true);
        }
    }
}