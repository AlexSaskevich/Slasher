using Source.InputSource;
using Source.Interfaces;
using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private InputSwitcher _inputSwitcher;
        private IInputSource _inputSource;

        private void Awake()
        {
            _inputSwitcher = GetComponent<InputSwitcher>();
        }

        private void Start()
        {
            _inputSource = _inputSwitcher.InputSource;
        }

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (_inputSource.MovementInput.x == 0 && _inputSource.MovementInput.z == 0)
                return;

            if (Vector3.Angle(transform.forward, _inputSource.MovementInput) <= 0)
                return;

            var newDirection = Vector3.RotateTowards(transform.forward, _inputSource.MovementInput, _speed * Time.deltaTime, 0);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}