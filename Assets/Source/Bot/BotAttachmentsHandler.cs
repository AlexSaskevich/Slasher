using BehaviorDesigner.Runtime;
using Source.Bot.Slicing;
using UnityEngine;

namespace Source.Bot
{
    [RequireComponent(typeof(Animator), typeof(BotHealth), typeof(BehaviorTree))]
    [RequireComponent(typeof(BotMovement))]
    public sealed class BotAttachmentsHandler : MonoBehaviour
    {
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Attachment[] _attachments;

        private void Awake()
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            TryGetAttachments();
        }

        private void OnEnable()
        {
            EnableAttachments();
        }

        public void DisableAttachments()
        {
            _skinnedMeshRenderer.gameObject.SetActive(false);
            
            if (_attachments != null)
                SetAttachments(false);
        }

        private void EnableAttachments()
        {
            _skinnedMeshRenderer.gameObject.SetActive(true);

            if (_attachments != null)
                SetAttachments(true);
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