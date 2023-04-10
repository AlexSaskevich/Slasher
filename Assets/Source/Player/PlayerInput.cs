using Source.Combo;
using Source.Constants;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerRotation), typeof(PlayerCombo))]
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        private PlayerMovement _playerMovement;
        private PlayerRotation _playerRotation;
        private PlayerCombo _playerCombo;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerRotation = GetComponent<PlayerRotation>();
            _playerCombo = GetComponent<PlayerCombo>();
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

            _playerCombo.SetAttack(Input.GetMouseButtonDown(0));
            _playerMovement.Move(horizontal, vertical);
            _playerRotation.Rotate(horizontal, vertical);
        }
    }
}