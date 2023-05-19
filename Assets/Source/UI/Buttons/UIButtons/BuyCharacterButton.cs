using Source.GameLogic;
using Source.Player;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Action = System.Action;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class BuyCharacterButton : UIButton
    {
        [SerializeField] private List<CharacterButton> _characterButtons;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _buyText;
        [SerializeField] private TMP_Text _selectText;
        [SerializeField] private PlayerDescription[] _playerDescriptions;
        [SerializeField] private MonoBehaviour _boosters;
        
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
            
            _selectText.gameObject.SetActive(true);
            _buyText.gameObject.SetActive(false);
        }

        private void Select()
        {
            GameProgressSaver.SetCurrentCharacterIndex((int)_playerCharacter.PlayerCharacterName);
            
            foreach (var playerDescription in _playerDescriptions)
                playerDescription.gameObject.SetActive(false);

            _boosters.gameObject.SetActive(true);
            
            Button.interactable = false;
            CharacterSet?.Invoke();
        }
        
        private void OnPlayerCharacterButtonChanged(PlayerCharacter playerCharacter)
        {
            _playerCharacter = playerCharacter;

            if (_playerCharacter.IsBought)
            {
                _selectText.gameObject.SetActive(true);
                _buyText.gameObject.SetActive(false);

                Button.interactable = GameProgressSaver.GetCurrentCharacterIndex() !=
                                      (int)playerCharacter.PlayerCharacterName;
            }
            else
            {
                _selectText.gameObject.SetActive(false);
                _buyText.gameObject.SetActive(true);

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