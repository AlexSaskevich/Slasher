using Source.Interfaces;
using Source.Skills;
using Source.UI.Buttons;
using System.Collections.Generic;
using UnityEngine;

namespace Source.InputSource
{
    public class UIInput : MonoBehaviour, IInputSource
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<ControlButton> _controlButtons;

        private Roll _roll;
        private Buff _buff;
        private Ultimate _ultimate;

        public Vector3 MovementInput { get; private set; }
        public bool IsAttackButtonClicked { get; private set; }

        private void Awake()
        {
            _roll = GetComponent<Roll>();
            _buff = GetComponent<Buff>();
            _ultimate = GetComponent<Ultimate>();
        }

        private void OnEnable()
        {
            foreach (var button in _controlButtons)
            {
                button.ControlButtonPressed += OnControlButtonPressed;
                button.ControlButtonReleased += OnControlButtonReleased;
            }
        }

        private void OnDisable()
        {
            foreach (var button in _controlButtons)
            {
                button.ControlButtonPressed -= OnControlButtonPressed;
                button.ControlButtonReleased -= OnControlButtonReleased;
            }
        }

        private void Update()
        {
            MovementInput = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        }

        public void HideButtons()
        {
            foreach (var button in _controlButtons)
                button.gameObject.SetActive(false);
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
            if (controlButton is AttackButton)
                IsAttackButtonClicked = true;
            else if (controlButton is RollButton)
                _roll.TryActivate();
            else if (controlButton is BuffButton)
                _buff.TryActivate();
            else if (controlButton is UltimateButton)
                _ultimate.TryActivate();
        }

        private void OnControlButtonReleased(ControlButton controlButton)
        {
            if (controlButton is AttackButton)
                IsAttackButtonClicked = false;
        }
    }
}