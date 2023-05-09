using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Buttons.ControlButtons
{
    public class ControlButton : MonoBehaviour
    {
        private Button _button;
        
        public event Action<ControlButton> ControlButtonPressed;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnControlButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnControlButtonClick);
        }

        private void OnControlButtonClick()
        {
            ControlButtonPressed?.Invoke(this);
        }
    }
}