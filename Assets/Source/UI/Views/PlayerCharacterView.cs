using Source.Player;
using Source.UI.Buttons.UIButtons;
using TMPro;
using UnityEngine;

namespace Source.UI.Views
{
    public class PlayerCharacterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerCharacterName;
        [SerializeField] private TMP_Text _playerCharacterPrice;
        [SerializeField] private CharacterButton[] _characterButtons;

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

        public void Set(PlayerCharacter playerCharacter)
        {
            _playerCharacterName.text = playerCharacter.PlayerCharacterName.ToString().ToUpper();
            _playerCharacterPrice.text = playerCharacter.Price.ToString();
        }

        private void OnPlayerCharacterChanged(PlayerCharacter playerCharacter)
        {
            Set(playerCharacter);
        }
    }
}