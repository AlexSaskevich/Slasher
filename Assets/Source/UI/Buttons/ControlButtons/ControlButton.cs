using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Buttons.ControlButtons
{
    public class ControlButton : MonoBehaviour
    {
        public event Action<ControlButton> ControlButtonPressed;

        [field: SerializeField] public Button Button { get; private set; }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnControlButtonClick);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnControlButtonClick);
        }

        private void OnControlButtonClick()
        {
            ControlButtonPressed?.Invoke(this);
        }
    }
}