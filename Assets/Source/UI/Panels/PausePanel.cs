using Source.UI.Buttons.UIButtons;
using UnityEngine;

namespace Source.UI.Panels
{
    public sealed class PausePanel : MonoBehaviour
    {
        [SerializeField] private ContinueButton _continueButton;
        [SerializeField] private ExitButton _exitButton;
        [SerializeField] private MuteSoundsButton _muteSoundsButton;

        private void Awake()
        {
            _continueButton.transform.SetParent(transform);
            _exitButton.transform.SetParent(transform);
            _muteSoundsButton.transform.SetParent(transform);
        }

        private void OnEnable()
        {
            _continueButton.Continued += OnContinued;
        }

        private void OnDisable()
        {
            _continueButton.Continued += OnContinued;
        }

        private void OnContinued()
        {
            gameObject.SetActive(false);
        }
    }
}