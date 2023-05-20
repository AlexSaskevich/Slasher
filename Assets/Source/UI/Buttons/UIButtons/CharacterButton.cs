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
        [SerializeField] private PlayerDescription[] _playerDescriptions;
        [SerializeField] private MonoBehaviour _boosters;
 
        public event Action<PlayerCharacter> PlayerCharacterChanged;

        protected override void OnEnable()
        {
            base.OnEnable();

            var currentPlayerCharacter = _playerCharacterSpawner.GetPlayerCharacters()
                .FirstOrDefault(character => character.gameObject.activeSelf);
            
            PlayerCharacterChanged?.Invoke(currentPlayerCharacter);
        }

        private void Start()
        {
            foreach (var playerDescription in _playerDescriptions)
                playerDescription.gameObject.SetActive(false);
        }

        protected void ChooseCharacter(bool isScrollingForward)
        {
            var playerCharacters = _playerCharacterSpawner.GetPlayerCharacters();
            
            var playerCharacterNames = new List<int>
            {
                (int)PlayerCharacterName.Biker, (int)PlayerCharacterName.Medic, (int)PlayerCharacterName.Ninja,
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

            _playerCharacterSpawner.RotateCharacter(newPlayerCharacter);
            
            currentPlayerCharacter.gameObject.SetActive(false);
            newPlayerCharacter.gameObject.SetActive(true);

            _boosters.gameObject.SetActive(false);

            if ((int)newPlayerCharacter.PlayerCharacterName != GameProgressSaver.GetCurrentCharacterIndex())
            {
                print("ADLALAKHJDFKLAD");
                foreach (var playerDescription in _playerDescriptions)
                {
                    if (newPlayerCharacter.PlayerCharacterName == playerDescription.PlayerCharacterName)
                    {
                        playerDescription.gameObject.SetActive(true);
                        continue;
                    }

                    playerDescription.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (var playerDescription in _playerDescriptions)
                    playerDescription.gameObject.SetActive(false);
                
                _boosters.gameObject.SetActive(true);
            }
            
            PlayerCharacterChanged?.Invoke(newPlayerCharacter);
        }
    }
}