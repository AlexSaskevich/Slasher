using Source.Player;
using Source.UI.Buttons.UIButtons;
using UnityEngine;

namespace Source.UI.Buttons.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class DeathPanel : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private RestartButton _restartButton;
        [SerializeField] private RegenerationButton _regenerationButton;
        
        private CanvasGroup _canvasGroup;
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        private void Start()
        {
            _restartButton.transform.SetParent(transform);
            _regenerationButton.transform.SetParent(transform);
        }

        public void Init(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            
            _playerHealth.Died += OnDied;
        }   
        
        private void OnDied()
        {
            Time.timeScale = 0;
            _canvasGroup.alpha = 1;
            
            _joystick.gameObject.SetActive(false);
        }
    }
}