using Source.GameLogic;
using Source.Player;
using Source.UI.Buttons.UIButtons;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    public class PlayerCharacterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerCharacterPrice;
        [SerializeField] private CharacterButton[] _characterButtons;
        [SerializeField] private List<PlayerCharacterNameView> _playerCharacterNameViews;

        private void OnEnable()
        {
            foreach (var button in _characterButtons)
                button.PlayerCharacterChanged += OnPlayerCharacterChanged;
        }

        private void OnDisable()
        {
            foreach (var button in _characterButtons)
                button.PlayerCharacterChanged -= OnPlayerCharacterChanged;
        }

        private void Start()
        {
            foreach (var playerCharacterNameView in _playerCharacterNameViews)
            {
                if ((int)playerCharacterNameView.PlayerCharacterName == GameProgressSaver.GetCurrentCharacterIndex())
                {
                    playerCharacterNameView.gameObject.SetActive(true);
                    continue;
                }

                playerCharacterNameView.gameObject.SetActive(false);
            }
        }

        public void Set(PlayerCharacter playerCharacter)
        {
            _playerCharacterPrice.text = playerCharacter.Price.ToString();

            foreach (var playerCharacterNameView in _playerCharacterNameViews)
            {
                if (playerCharacterNameView.PlayerCharacterName == playerCharacter.PlayerCharacterName)
                {
                    playerCharacterNameView.gameObject.SetActive(true);
                    continue;
                }

                playerCharacterNameView.gameObject.SetActive(false);
            }
        }

        private void OnPlayerCharacterChanged(PlayerCharacter playerCharacter)
        {
            Set(playerCharacter);
        }
    }
}