using Source.Interfaces;
using UnityEngine;

namespace Source.InputSource
{
    [RequireComponent(typeof(KeyboardInput), typeof(UIInput))]
    public class InputSwitcher : MonoBehaviour
    {
        [SerializeField] private DeviceType _deviceType;

        private Joystick _joystick;

        public IInputSource InputSource { get; private set; }

        private void Awake()
        {
            GetInputSource();
        }

        public void Init(Joystick joystick)
        {
            _joystick = joystick;
        }

        private void GetInputSource()
        {
            switch (_deviceType)
            {
                case DeviceType.Desktop:
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
            uiInput.HideButtons();
            uiInput.enabled = false;

            if (_joystick == null)
                return;

            _joystick.gameObject.SetActive(false);
        }

        private void DisableKeyboardInput()
        {
            var keyboardInput = GetComponent<KeyboardInput>();
            keyboardInput.enabled = false;
        }
    }
}