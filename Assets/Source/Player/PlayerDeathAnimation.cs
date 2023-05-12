using UnityEngine;

namespace Source.Player
{
    public sealed class PlayerDeathAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationClip _death;

        public float GetLenght()
        {
            return _death.length;
        }
    }
}