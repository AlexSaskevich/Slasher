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

            var direction = new Vector3(horizontal, 0, vertical);
            
            _playerMovement.Move(direction);
            _playerRotation.Rotate(horizontal, vertical);
        }
    }
}