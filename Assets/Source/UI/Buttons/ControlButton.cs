using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Source.UI.Buttons
{
    public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<ControlButton> ControlButtonPressed;
        public event Action<ControlButton> ControlButtonReleased;

        protected Button Button { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ControlButtonPressed?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ControlButtonReleased?.Invoke(this);
        }
    }
}