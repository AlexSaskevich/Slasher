using Source.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Test
{
    public class BotSlicer : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private float _force;
        [SerializeField] private bool _hasAttachments;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _body;

        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Health _botHealth;
        private Animator _animator;
        private Attachment[] _attachments;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _botHealth = GetComponent<Health>();
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            TryGetAttachments();
        }

        private void OnEnable()
        {
            _botHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _botHealth.Died -= OnDied;
        }

        private void Start()
        {
            for (int i = 0; i < _parts.Length; i++)
                _parts[i].gameObject.SetActive(false);
        }

        private void OnDied()
        {
            if (_botHealth.enabled == false)
                return;

            DisableBot();

            EnableBotSlices();
        }

        private void DisableBot()
        {
            _botHealth.enabled = false;
            _skinnedMeshRenderer.gameObject.SetActive(false);
            _animator.enabled = false;

            TrySetAttachmentsParent();

            for (int i = 0; i < _colliders.Length; i++)
                _colliders[i].enabled = false;
        }

        private void EnableBotSlices()
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].gameObject.SetActive(true);
                _parts[i].transform.parent = transform;
                _parts[i].AddForce(Vector3.up * _force);
            }
        }

        private void TryGetAttachments()
        {
            if (_hasAttachments)
                _attachments = transform.GetComponentsInChildren<Attachment>();
        }

        private void TrySetAttachmentsParent()
        {
            List<Transform> bodyAttachments = new();
            List<Transform> headAttachments = new();

            for (int i = 0; i < _attachments.Length; i++)
            {
                if (_attachments[i] as BodyAttachment)
                    bodyAttachments.Add(_attachments[i].transform);
                else
                    headAttachments.Add(_attachments[i].transform);
            }

            foreach (var bodyAttachment in bodyAttachments)
                bodyAttachment.transform.SetParent(_body);

            foreach (var headAttachment in headAttachments)
                headAttachment.transform.SetParent(_head);
        }
    }
}