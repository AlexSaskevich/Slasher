using Source.Constants;
using Source.Interfaces;
using Source.Skills;
using UnityEngine;

namespace Source.InputSource
{
    public class KeyboardInput : MonoBehaviour, IInputSource
    {
        private Roll _roll;
        private Buff _buff;
        private Ultimate _ultimate;

        public Vector3 MovementInput { get; private set; }
        public bool IsAttackButtonClicked { get; private set; }

        private void Awake()
        {
            _roll = GetComponent<Roll>();
            _buff = GetComponent<Buff>();
            _ultimate = GetComponent<Ultimate>();
        }

        private void Update()
        {
            MovementInput = new Vector3(Input.GetAxisRaw(InputConstants.Horizontal), 0, Input.GetAxisRaw(InputConstants.Vertical));

            if (_roll.IsActive)
                return;

            IsAttackButtonClicked = Input.GetMouseButtonDown(0);

            if (Input.GetKeyDown(KeyCode.Space))
                _roll.TryActivate();

            if (Input.GetKeyDown(KeyCode.V))
                _buff.TryActivate();

            if (Input.GetKeyDown(KeyCode.F))
                _ultimate.TryActivate();
        }

        public void Disable()
        {
            enabled = false;
            MovementInput = Vector3.zero;
        }

        public void Enable()
        {
            enabled = true;
        }
    }
}