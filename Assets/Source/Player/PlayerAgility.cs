using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerAgility : MonoBehaviour
    {
        [SerializeField] private float _maxAgility;
        [SerializeField] private float _increasingAgility;
        
        public float Agility { get; private set; }

        private void Start()
        {
            Agility = _maxAgility;
        }

        private void Update()
        {
            if (Agility >= _maxAgility)
                return;
            
            Agility += _increasingAgility;
        }

        public void TryTakeAgility(float value)
        {
            if (value <= 0)
                return;

            Agility -= value;
        }
    }
}