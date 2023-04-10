using Source.Player;
using UnityEngine;

public class PlayerManaBar : Bar
{
    [SerializeField] private PlayerMana _playerMana;
    private void Awake()
    {
        Initialize(_playerMana.MaxMana);
    }

    private void OnEnable()
    {
        _playerMana.ManaChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _playerMana.ManaChanged -= OnValueChanged;
    }

    protected override void OnValueChanged()
    {
        float targetValue = (float)_playerMana.Mana / _playerMana.MaxMana;

        StartChangeFillAmount(targetValue);
    }
}