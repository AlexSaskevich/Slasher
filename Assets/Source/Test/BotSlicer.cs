using Source.Bot;
using Source.GameLogic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Test
{
    public class BotSlicer : MonoBehaviour
    {
        private const int BotSliceLayer = 9;

        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private float _force;

        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Health _botHealth;
        private Animator _animator;
        private Attachment[] _attachments;
        private List<Slice> _parents;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _botHealth = GetComponent<Health>();
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

            if (TryGetComponent(out BotRangedAttacker botRangedAttacker) == false)
                return;

            GetAttachments();
            GetAttachmentsParent();
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

            for (int i = 0; i < _colliders.Length; i++)
                _colliders[i].enabled = false;

            if (_attachments != null)
                SetAttachmentsParent();
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

        private void GetAttachments()
        {
            _attachments = transform.GetComponentsInChildren<Attachment>();
        }

        private void GetAttachmentsParent()
        {
            _parents = GetComponentsInChildren<Slice>().ToList();
        }

        private void SetAttachmentsParent()
        {
            for (int i = 0; i < _attachments.Length; i++)
            {
                if (_attachments[i] as BodyAttachment)
                    _attachments[i].transform.SetParent(_parents.Find(p => p is BodySlice).transform);
                else if (_attachments[i] as HeadAttachment)
                    _attachments[i].transform.SetParent(_parents.Find(p => p is HeadSlice).transform);
                else
                    _attachments[i].transform.SetParent(_parents.Find(p => p is ArmSlice).transform);

                _attachments[i].gameObject.layer = BotSliceLayer;
            }
        }
    }
}