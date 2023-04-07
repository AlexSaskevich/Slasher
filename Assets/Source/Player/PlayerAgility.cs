using Source.Combo;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Player
{
    public sealed class PlayerAgility : MonoBehaviour
    {
        [SerializeField] private PlayerCombo _playerCombo;
        [SerializeField] private float _increasingAgility;
        [SerializeField] private float _decreasingAgility;

        public event UnityAction AgilityChanged;

        [field: SerializeField] public float MaxAgility { get; private set; }
        public float Agility { get; private set; }

        private void Awake()
        {
            Agility = MaxAgility;
        }

        private void OnEnable()
        {
            _playerCombo.Attacked += OnAttacked;
        }

        private void OnDisable()
        {
            _playerCombo.Attacked -= OnAttacked;
        }

        private void Update()
        {
            if (_playerCombo.CurrentState is IdleState && Agility < MaxAgility)
                IncreaseAgility();
        }

        private void OnAttacked()
        {
            DecreaseAgility();
        }

        private void IncreaseAgility()
        {
            Agility = Mathf.Clamp(Agility + _increasingAgility, 0, MaxAgility);
            AgilityChanged?.Invoke();
        }

        private void DecreaseAgility()
        {
            Agility = Mathf.Clamp(Agility - _decreasingAgility, 0, MaxAgility);
            AgilityChanged?.Invoke();
        }
    }
}