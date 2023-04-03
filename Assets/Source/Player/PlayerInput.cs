using Source.Constants;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerRotation))]
    public sealed class PlayerInput : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerRotation _playerRotation;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerRotation = GetComponent<PlayerRotation>();
        }

        private void Update()
        {
            var horizontal = Input.GetAxisRaw(InputConstants.Horizontal);
            var vertical = Input.GetAxisRaw(InputConstants.Vertical);

            _playerMovement.Move(horizontal, vertical);
            _playerRotation.Rotate(horizontal, vertical);
        }
    }
}