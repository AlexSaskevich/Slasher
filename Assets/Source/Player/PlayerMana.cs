using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerMana : MonoBehaviour
    {
        [SerializeField] private float _maxMana;
        [SerializeField] private float _increasingMana;

        public float Mana { get; private set; }

        private void Start()
        {
            Mana = _maxMana;
        }

        private void Update()
        {
            if (Mana >= _maxMana)
                return;

            Mana += _increasingMana;
        }

        public void TryTakeMana(float value)
        {
            if (value <= 0)
                return;

            Mana -= _increasingMana;
        }
    }
}