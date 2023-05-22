using Source.Turntables;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Buttons.UIButtons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] private ButtonTurntable _buttonTurntable;
        
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

        protected virtual void OnButtonClick()
        {
            _buttonTurntable.PlayButtonSound();
        }
    }
}