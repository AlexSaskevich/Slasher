using System.Collections.Generic;
using UnityEngine;

namespace Source.Bot.Slicing
{
    [RequireComponent(typeof(BotHealth))]
    public sealed class BotSlicer : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private float _force;
        [SerializeField] private bool _hasAttachments;

        private readonly List<Rigidbody> _pool = new();

        private BotHealth _botHealth;
        private BotAttachmentsHandler _botAttachmentsHandler;
        private Coroutine _coroutine;

        private void Awake()
        {
            _botHealth = GetComponent<BotHealth>();

            if (_hasAttachments)
                _botAttachmentsHandler = GetComponent<BotAttachmentsHandler>();
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
            DisableParts();
            CreateParts();
        }

        private void ConstructParts()
        {
            for (var i = 0; i < _pool.Count; i++)
            {
                var part = _pool[i];
                part.gameObject.SetActive(false);

                part.transform.SetParent(_parts[i].transform.parent);
                part.transform.SetLocalPositionAndRotation(_parts[i].transform.localPosition,
                    _parts[i].transform.localRotation);
            }
        }

        private void CreateParts()
        {
            foreach (var part in _parts)
            {
                var newPart = Instantiate(part, part.position, part.rotation, part.transform.parent);
                newPart.gameObject.SetActive(false);
                
                _pool.Add(newPart);
            }
        }

        private void OnDied()
        {
            if (_botHealth.enabled == false)
                return;

            if (_hasAttachments)
                _botAttachmentsHandler.DisableAttachments();

            ConstructParts();
            EnableBotSlices();
        }

        private void DisableParts()
        {
            foreach (var part in _parts)
                part.gameObject.SetActive(false);
        }
        
        private void EnableBotSlices()
        {
            foreach (var part in _pool)
            {
                part.gameObject.SetActive(true);
                part.transform.parent = null;
                part.AddForce(Vector3.up * _force);
            }
        }
    }
}