using Source.Constants;
using Source.Interfaces;
using Source.Skills;
using Source.UI.Buttons.ControlButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.InputSource
{
    public class UIInput : MonoBehaviour, IInputSource
    {
        private List<ControlButton> _controlButtons;
        private Joystick _joystick;
        private Roll _roll;
        private Buff _buff;
        private Ultimate _ultimate;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;

        public Vector3 MovementInput { get; private set; }
        public bool IsAttackButtonClicked { get; private set; }

        private void Awake()
        {
            _roll = GetComponent<Roll>();
            _buff = GetComponent<Buff>();
            _ultimate = GetComponent<Ultimate>();
            _waitForSeconds = new WaitForSeconds(InputConstants.WaitTime);
        }

        private void OnEnable()
        {
            if (_controlButtons == null)
                return;

            foreach (var button in _controlButtons)
                button.ControlButtonPressed += OnControlButtonPressed;
        }

        private void OnDisable()
        {
            if (_controlButtons == null)
                return;

            foreach (var button in _controlButtons)
                button.ControlButtonPressed -= OnControlButtonPressed;
        }

        private void Update()
        {
            if (_joystick == null)
                return;

            MovementInput = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        }

        public void Init(Joystick joystick, IEnumerable<ControlButton> controlButtons)
        {
            _joystick = joystick;

            _controlButtons = new List<ControlButton>();

            foreach (var controlButton in controlButtons)
                _controlButtons.Add(controlButton);
        }

        public void Hide()
        {
            if (_controlButtons == null)
                return;

            foreach (var button in _controlButtons)
                button.gameObject.SetActive(false);

            _joystick.gameObject.SetActive(false);
        }

        public void Disable()
        {
            enabled = false;
            MovementInput = Vector3.zero;
        }

        public void Enable()
        {
            enabled = true;
            _joystick.enabled = true;
        }

        private void OnControlButtonPressed(ControlButton controlButton)
        {
            if (_roll.IsActive)
                return;

            if (_buff.IsActive)
                return;

            if (_ultimate.IsActive)
                return;

            switch (controlButton)
            {
                case AttackButton:
                    IsAttackButtonClicked = true;
                    StartWait();
                    break;
                case RollButton:
                    _roll.TryActivate();
                    break;
                case BuffButton:
                    _buff.TryActivate();
                    break;
                case UltimateButton:
                    _ultimate.TryActivate();
                    break;
            }
        }

        private void StartWait()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return _waitForSeconds;
            IsAttackButtonClicked = false;
        }
    }
}