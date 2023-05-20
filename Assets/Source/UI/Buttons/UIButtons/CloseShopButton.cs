using Source.GameLogic;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class CloseShopButton : UIButton
    {
        [SerializeField] private PlayerCharacterSpawner _playerCharacterSpawner;
        [SerializeField] private CharacterButton _characterButton;
        
        protected override void OnButtonClick()
        {
            CloseShop();
        }

        private void CloseShop()
        {
            var currentCharacterIndex = GameProgressSaver.GetCurrentCharacterIndex();

            foreach (var playerCharacter in _playerCharacterSpawner.GetPlayerCharacters())
            {
                if (currentCharacterIndex == (int)playerCharacter.PlayerCharacterName)
                {
                    _playerCharacterSpawner.SetCurrentPlayer();
                    continue;
                }
                
                playerCharacter.gameObject.SetActive(false);
            }
            
            _characterButton.EnableBoosters();
        }
    }
}