using Source.GameLogic;
using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class CloseShopButton : UIButton
    {
        [SerializeField] private PlayerCharacterSpawner _playerCharacterSpawner;
        [SerializeField] private CharacterButton _characterButton;
        [SerializeField] private PlayerCharacterView _playerCharacterView;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
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
                    _playerCharacterView.Set(playerCharacter);
                    continue;
                }
                
                playerCharacter.gameObject.SetActive(false);
            }
            
            _characterButton.EnableBoosters();
        }
    }
}