using Source.Interfaces;
using UnityEngine;
using DeviceType = Agava.YandexGames.DeviceType;

namespace Source.InputSource
{
    [RequireComponent(typeof(KeyboardInput), typeof(UIInput))]
    public class InputSwitcher : MonoBehaviour
    {
        public IInputSource InputSource { get; private set; }

        public void Init(DeviceType deviceType)
        {
            GetInputSource(deviceType);
        }

        private void GetInputSource(DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.Desktop:
                    InputSource = GetComponent<KeyboardInput>();
                    DisableUIInput();
                    break;
                default:
                    InputSource = GetComponent<UIInput>();
                    DisableKeyboardInput();
                    break;
            }
        }

        private void DisableUIInput()
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