using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(Animator), typeof(BotHealth), typeof(BehaviorTree))]
    public sealed class BotDeathHandler : MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders;
        
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private BehaviorTree _behaviorTree;
        private BotHealth _botHealth;
        private Animator _animator;

        private void Awake()
        {
            _botHealth = GetComponent<BotHealth>();
            _animator = GetComponent<Animator>();
            _behaviorTree = GetComponent<BehaviorTree>();
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        private void OnEnable()
        {
            EnableBot();
        }

        public void DisableBot()
        {
            SetBotState(false);
        }

        public void EnableBot()
        {
            SetBotState(true);
        }
        
        private void SetBotState(bool isEnabled)
        {
            _botHealth.enabled = isEnabled;
            _animator.enabled = isEnabled;
            _behaviorTree.enabled = isEnabled;
            _skinnedMeshRenderer.gameObject.SetActive(isEnabled);

            foreach (var collider in _colliders)
                collider.enabled = isEnabled;
        }
    }
}