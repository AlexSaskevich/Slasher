using UnityEngine;

namespace Source.Interfaces
{
    public interface IMoveable
    {
        float Speed { get; }

        void Move(Vector3 direction);
    }
}