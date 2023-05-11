using Source.Interfaces;
using UnityEngine;

namespace Source.InputSource
{
    [RequireComponent(typeof(KeyboardInput), typeof(UIInput))]
    public class InputSwitcher : MonoBehaviour
    {
        public IInputSource InputSource { get; private set; }

        public void Init(Agava.YandexGames.DeviceType deviceType)
        {
            switch (deviceType) 
            {
                case Agava.YandexGames.DeviceType.Desktop:
                    InputSource = GetComponent<KeyboardInput>();
                    DisableUIINput();
                    break;
                default:
                    InputSource = GetComponent<UIInput>();
                    DisableKeyboardInput();
                    break;
            }
        }

        private void DisableUIINput()
        {
            var uiInput = GetComponent<UIInput>();
            uiInput.Hide();
            uiInput.enabled = false;
        }

        private void DisableKeyboardInput()
        {
            var keyboardInput = GetComponent<KeyboardInput>();
            keyboardInput.enabled = false;
        }
    }
}