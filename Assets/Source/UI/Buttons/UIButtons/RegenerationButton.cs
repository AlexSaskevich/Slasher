using Source.Combo;
using Source.GameLogic.Timers;
using Source.Interfaces;
using Source.Player;
using Source.Yandex;
using System;
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

        protected override void OnEnable()
        {
            base.OnEnable();

            _adShower.AdvertisementClosed += OnAdvertisementClosed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _adShower.AdvertisementClosed -= OnAdvertisementClosed;
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();

            _adShower.Show();
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

        private void OnAdvertisementClosed()
        {
            Regenerate();
        }

        private void Regenerate()
        {
            IsClicked = true;

            _playerHealth.ResetHealth();
            _playerMana.ResetMana();
            _playerAgility.ResetAgility();

            _playerCombo.enabled = true;
            _playerCombo.SwitchState(_playerCombo.MoveState);
            _inputSource.Enable();

            _animator.Rebind();

            if (_timerBlinder != null)
                _timerBlinder.SetPlayerAlive();

            PlayerRegenerated?.Invoke();
        }
    }
}