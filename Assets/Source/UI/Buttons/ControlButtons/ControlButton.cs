using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Buttons.ControlButtons
{
    public class ControlButton : MonoBehaviour
    {
        public event Action<ControlButton> ControlButtonPressed;

        protected Button Button { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnControlButtonClick);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnControlButtonClick);
        }

        protected void OnControlButtonClick()
        {
            ControlButtonPressed?.Invoke(this);
        }
    }
}