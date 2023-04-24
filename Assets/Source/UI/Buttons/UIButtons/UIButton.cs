using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Buttons.UIButtons
{
    public abstract class UIButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnUIButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnUIButtonClick);
        }

        protected abstract void OnUIButtonClick();
    }
}