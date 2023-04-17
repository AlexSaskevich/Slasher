using Source.Combo;
using Source.Constants;
using Source.Skills;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerRotation), typeof(PlayerCombo))]
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        private PlayerMovement _playerMovement;
        private PlayerRotation _playerRotation;
        private Roll _roll;
        private Buff _buff;
        private Ultimate _ultimate;

        public bool IsAttackButtonClicked { get; private set; }

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerRotation = GetComponent<PlayerRotation>();
            _roll = GetComponent<Roll>();
            _buff = GetComponent<Buff>();
            _ultimate = GetComponent<Ultimate>();
        }

        private void Update()
        {
            float horizontal;
            float vertical;

            if (_joystick.Horizontal == 0 || _joystick.Vertical == 0)
            {
                horizontal = Input.GetAxisRaw(InputConstants.Horizontal);
                vertical = Input.GetAxisRaw(InputConstants.Vertical);
            }
            else
            {
                horizontal = _joystick.Horizontal;
                vertical = _joystick.Vertical;
            }

            _playerMovement.Move(horizontal, vertical);
            _playerRotation.Rotate(horizontal, vertical);

            IsAttackButtonClicked = Input.GetMouseButtonDown(0);

            if (Input.GetKeyDown(KeyCode.Space))
                _roll.TryActivate();

            if (Input.GetKeyDown(KeyCode.V))
                _buff.TryActivate();

            if (Input.GetKeyDown(KeyCode.F))
                _ultimate.TryActivate();
        }
    }
}