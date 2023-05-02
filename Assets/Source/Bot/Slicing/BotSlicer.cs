using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Bot.Slicing
{
    [RequireComponent(typeof(BotDeathHandler), typeof(BotHealth))]
    public sealed class BotSlicer : MonoBehaviour
    {
        private const int BotSliceLayer = 9;

        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private float _force;
        [SerializeField] private float _timeToDestroy;

        private Vector3[] _partsPositions;
        private Transform[] _partsParents;
        private Quaternion[] _partsRotations;
        private Attachment[] _attachments;
        private List<Slice> _parents;
        private BotHealth _botHealth;
        private BotDeathHandler _botDeathHandler;
        private Coroutine _coroutine;

        private void Awake()
        {
            _botHealth = GetComponent<BotHealth>();
            _botDeathHandler = GetComponent<BotDeathHandler>();
            
            if (TryGetComponent(out BotRangedAttacker _) == false)
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
            InitParts();
        }

        public void ConstructParts()
        {
            if (_partsPositions == null)
                return;

            for (var i = 0; i < _partsPositions.Length; i++)
            {
                _parts[i].gameObject.SetActive(false);

                _parts[i].transform.parent = _partsParents[i];
                _parts[i].transform.localPosition = _partsPositions[i];
                _parts[i].transform.localRotation = _partsRotations[i];
            }
        }

        private void InitParts()
        {
            _partsParents = new Transform[_parts.Length];
            _partsPositions = new Vector3[_parts.Length];
            _partsRotations = new Quaternion[_parts.Length];
            
            for (var i = 0; i < _parts.Length; i++)
            {
                _parts[i].gameObject.SetActive(false);

                _partsParents[i] = _parts[i].transform.parent;
                _partsPositions[i] = _parts[i].transform.localPosition;
                _partsRotations[i] = _parts[i].transform.localRotation;
            }
        }
      
        private void OnDied()
        {
            if (_botHealth.enabled == false)
                return;
            
            _botDeathHandler.DisableBot();

            if (_attachments != null)
                SetAttachmentsParent();
            
            EnableBotSlices();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitTimeToDeleteSlices());
        }
        
        private void EnableBotSlices()
        {
            foreach (var part in _parts)
            {
                part.gameObject.SetActive(true);
                part.transform.parent = transform;
                part.AddForce(Vector3.up * _force);
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
            foreach (var attachment in _attachments)
            {
                if (attachment as BodyAttachment)
                    attachment.transform.SetParent(_parents.Find(p => p is BodySlice).transform);
                else if (attachment as HeadAttachment)
                    attachment.transform.SetParent(_parents.Find(p => p is HeadSlice).transform);
                else
                    attachment.transform.SetParent(_parents.Find(p => p is ArmSlice).transform);

                attachment.gameObject.layer = BotSliceLayer;
            }
        }

        private IEnumerator WaitTimeToDeleteSlices()
        {
            yield return new WaitForSeconds(_timeToDestroy);
            
            gameObject.SetActive(false);
        }
    }
}