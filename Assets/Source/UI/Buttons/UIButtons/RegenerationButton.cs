using System;
using Source.Combo;
using Source.GameLogic.Timers;
using Source.Interfaces;
using Source.Player;
using Source.Yandex;
using UnityEngine;

namespace Source.UI.Buttons.UIButtons
{
    public sealed class RegenerationButton : UIButton
    {
        [SerializeField] private TimerBlinder _timerBlinder;
        [SerializeField] private AdShower _adShower;
        
        private PlayerHealth _playerHealth;
        private PlayerCombo _playerCombo;
        private IInputSource _inputSource;
        private Animator _animator;
        private PlayerMana _playerMana;
        private PlayerAgility _playerAgility;

        public event Action PlayerRegenerated;
        
        public bool IsClicked { get; private set; }
        
        protected override void OnButtonClick()
        {
            RegeneratePlayer();
        }

        public void Init(PlayerHealth playerHealth, PlayerCombo playerCombo, IInputSource inputSource,
            Animator animator, PlayerMana playerMana, PlayerAgility playerAgility)
        {
            _playerHealth = playerHealth;
            _playerCombo = playerCombo;
            _inputSource = inputSource;
            _animator = animator;
            _playerMana = playerMana;
            _playerAgility = playerAgility;
        }

        public void SetInteractableState(bool state)
        {
            Button.interactable = state;
        }

        private void RegeneratePlayer()
        {
            IsClicked = true;
            
            _playerHealth.ResetHealth();
            _playerMana.ResetMana();
            _playerAgility.ResetAgility();
            
            _playerCombo.enabled = true;
            _playerCombo.SwitchState(_playerCombo.MoveState);
            _inputSource.Enable();
            
            _animator.Rebind();
            _timerBlinder.SetPlayerAlive();
            
            PlayerRegenerated?.Invoke();
            _adShower.Show();
        }
    }
}