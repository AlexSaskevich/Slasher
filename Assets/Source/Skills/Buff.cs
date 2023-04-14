using Source.Interfaces;
using Source.Player;
using UnityEngine;

namespace Source.Skills
{
    public sealed class Buff : Skill
    {
        [SerializeField] private MonoBehaviour _buffableBehaviour;
        [SerializeField] private float _modifier;
        [SerializeField] private float _duration;

        private IBuffable _buffable;
        private PlayerMana _playerMana;

        private void OnValidate()
        {
            if (_buffableBehaviour && _buffableBehaviour is IBuffable == false)
                Debug.LogError(nameof(_buffableBehaviour) + " needs to implement " + nameof(IBuffable));

            if (_duration > Cooldown)
                Debug.LogError(nameof(_duration) + " must be less then " + nameof(Cooldown));
        }

        private void Awake()
        {
            _buffable = (IBuffable)_buffableBehaviour;
            _playerMana = GetComponent<PlayerMana>();
            Initialization();
        }

        private void Update()
        {
            if (_buffable.IsBuffed == false)
                return;

            if (ElapsedTime <= Cooldown - _duration)
                _buffable.RemoveModifier(_modifier);
        }

        public override void TryActivate()
        {
            if (ElapsedTime < Cooldown)
                return;

            if (_playerMana.CurrentMana < Cost)
                return;

            _playerMana.DecreaseMana(Cost);

            _buffable.AddModifier(_modifier);

            StartTimer();
        }
    }
}