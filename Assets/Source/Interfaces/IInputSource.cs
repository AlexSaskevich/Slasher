using UnityEngine;

namespace Source.Interfaces
{
    public interface IInputSource
    {
        Vector3 MovementInput { get; }
        bool IsAttackButtonClicked { get; }

        void Enable();

        void Disable();
    }
}