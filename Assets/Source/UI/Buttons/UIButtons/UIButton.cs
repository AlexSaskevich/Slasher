using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Buttons.UIButtons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour
    {
        protected Button Button { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        protected virtual void OnEnable()
        {
            Button.onClick.AddListener(OnButtonClick);
        }

        protected virtual void OnDisable()
        {
            Button.onClick.RemoveListener(OnButtonClick);
        }

        protected abstract void OnButtonClick();
    }
}