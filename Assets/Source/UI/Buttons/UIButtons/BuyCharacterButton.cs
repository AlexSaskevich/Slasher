using System.Collections.Generic;
using Source.GameLogic;
using Source.Player;
using TMPro;
using UnityEngine;
using Action = System.Action;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class BuyCharacterButton : UIButton
    {
        private const string BuyCommand = "Buy";
        private const string SelectCommand = "Select";
        
        [SerializeField] private List<CharacterButton> _characterButtons;
        [SerializeField] private TMP_Text _text;
        
        private PlayerWallet _playerWallet;
        private PlayerCharacter _playerCharacter;

        public event Action CharacterSet;

        protected override void OnEnable()
        {
            base.OnEnable();

            foreach (var characterButton in _characterButtons)
                characterButton.PlayerCharacterChanged += OnPlayerCharacterButtonChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            foreach (var characterButton in _characterButtons)
                characterButton.PlayerCharacterChanged -= OnPlayerCharacterButtonChanged;
        }
        
        public void Init(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }
        
        protected override void OnButtonClick()
        {
            if (_playerCharacter.IsBought)
                Select();
            else
                Buy();
        }

        private void Buy()
        {
            _playerWallet.TrySpendMoney(_playerCharacter.Price);
            _playerCharacter.Buy();

            GameProgressSaver.SetCharacterBoughtStatus(_playerCharacter.PlayerCharacterName, true);
            
            _text.text = SelectCommand;
        }

        private void Select()
        {
            GameProgressSaver.SetCurrentCharacterIndex((int)_playerCharacter.PlayerCharacterName);

            Button.interactable = false;
            CharacterSet?.Invoke();
        }
        
        private void OnPlayerCharacterButtonChanged(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;

            if (_playerCharacter.IsBought)
            {
                _text.text = SelectCommand;
                Button.interactable = GameProgressSaver.GetCurrentCharacterIndex() !=
                                      (int)playerCharacter.PlayerCharacterName;
            }
            else
            {
                _text.text = BuyCommand;
                
                if (_playerWallet.CurrentMoney < _playerCharacter.Price)
                {
                    Button.interactable = false;
                    return;
                }
                
                Button.interactable = true;
            }
        }
    }
}