using System;
using System.Collections.Generic;
using System.Linq;
using Source.Enums;
using Source.GameLogic;
using Source.Player;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public abstract class CharacterButton : UIButton
    {
        [SerializeField] private PlayerCharacterSpawner _playerCharacterSpawner;

        public event Action<PlayerCharacter> PlayerCharacterChanged;

        protected override void OnEnable()
        {
            base.OnEnable();

            var currentPlayerCharacter = _playerCharacterSpawner.GetPlayerCharacters()
                .FirstOrDefault(character => character.gameObject.activeSelf);
            
            PlayerCharacterChanged?.Invoke(currentPlayerCharacter);
        }

        protected void ChooseCharacter(bool isScrollingForward)
        {
            var playerCharacters = _playerCharacterSpawner.GetPlayerCharacters();
            
            var playerCharacterNames = new List<int>
            {
                (int)PlayerCharacterName.Biker, (int)PlayerCharacterName.Medic, (int)PlayerCharacterName.Ninja,
                (int)PlayerCharacterName.Death
            };

            var currentPlayerCharacter = playerCharacters.FirstOrDefault(character => character.gameObject.activeSelf);

            if (currentPlayerCharacter == null)
                throw new ArgumentNullException();

            var currentPlayerCharacterName = (int)currentPlayerCharacter.PlayerCharacterName;
            var index = playerCharacterNames.IndexOf(currentPlayerCharacterName);

            int newIndex;

            if (isScrollingForward)
                newIndex = index == playerCharacterNames.Count - 1 ? 0 : index + 1;
            else
                newIndex = index == 0 ? playerCharacterNames.Count - 1 : index - 1;

            var newPlayerCharacter = _playerCharacterSpawner.TryGetPlayerCharacterByIndex(newIndex);
            
            if (newPlayerCharacter == null)
                throw new ArgumentNullException();
            
            currentPlayerCharacter.gameObject.SetActive(false);
            newPlayerCharacter.gameObject.SetActive(true);
            
            PlayerCharacterChanged?.Invoke(newPlayerCharacter);
        }
    }
}