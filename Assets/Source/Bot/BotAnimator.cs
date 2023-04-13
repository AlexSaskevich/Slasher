using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(Animator))]
    public sealed class BotAnimator : MonoBehaviour
    {
        public Animator Animator { get; private set; }
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }
    }
}