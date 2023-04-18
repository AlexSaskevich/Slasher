using Source.Interfaces;
using UnityEngine;

namespace Source.InputSource
{
    [RequireComponent(typeof(KeyboardInput), typeof(UIInput))]
    public class InputSwitcher : MonoBehaviour
    {
        [SerializeField] private DeviceType _deviceType;
        [SerializeField] private GameObject _joystick;

        public IInputSource InputSource { get; private set; }

        private void Awake()
        {
            GetInputSource();
        }

        private void GetInputSource()
        {
            switch (_deviceType)
            {
                case DeviceType.Desktop:
                    InputSource = GetComponent<KeyboardInput>();
                    var uiinput = GetComponent<UIInput>();
                    uiinput.HideButtons();
                    uiinput.enabled = false;
                    _joystick.SetActive(false);
                    break;
                default:
                    InputSource = GetComponent<UIInput>();
                    var keyboardInput = GetComponent<KeyboardInput>();
                    keyboardInput.enabled = false;
                    break;
            }
        }
    }
}