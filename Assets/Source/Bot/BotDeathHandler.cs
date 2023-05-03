using BehaviorDesigner.Runtime;
using Source.Bot.Slicing;
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
        private Attachment[] _attachments;

        private void Awake()
        {
            _botHealth = GetComponent<BotHealth>();
            _animator = GetComponent<Animator>();
            _behaviorTree = GetComponent<BehaviorTree>();
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            TryGetAttachments();
        }

        private void OnEnable()
        {
            EnableBot();
        }

        public void DisableBot()
        {
            SetBotState(false);

            if (_attachments != null)
                SetAttachments(false);
        }

        public void EnableBot()
        {
            SetBotState(true);

            if (_attachments != null)
                SetAttachments(true);
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

        private void TryGetAttachments()
        {
            if (TryGetComponent(out BotRangedAttacker _) == false)
                return;

            _attachments = transform.GetComponentsInChildren<Attachment>();
        }

        private void SetAttachments(bool isActive)
        {
            foreach (var attachment in _attachments)
                attachment.gameObject.SetActive(isActive);
        }
    }
}