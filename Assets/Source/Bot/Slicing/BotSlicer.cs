using UnityEngine;

namespace Source.Bot.Slicing
{
    [RequireComponent(typeof(BotHealth))]
    public sealed class BotSlicer : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private float _force;
        [SerializeField] private bool _hasAttachments;

        private Vector3[] _partsPositions;
        private Transform[] _partsParents;
        private Quaternion[] _partsRotations;
        private BotHealth _botHealth;
        private BotAttachmentsHandler _botAttachmentsHandler;
        private Coroutine _coroutine;
        private float _timer;

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
            InitParts();
        }

        public void ConstructParts()
        {
            if (_partsPositions == null)
                return;

            for (var i = 0; i < _partsPositions.Length; i++)
            {
                _parts[i].gameObject.SetActive(false);

                _parts[i].transform.SetParent(_partsParents[i]);
                _parts[i].transform.SetLocalPositionAndRotation(_partsPositions[i], _partsRotations[i]);
            }
        }

        private void InitParts()
        {
            _partsPositions = new Vector3[_parts.Length];
            _partsRotations = new Quaternion[_parts.Length];
            _partsParents = new Transform[_parts.Length];

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

            if (_hasAttachments)
                _botAttachmentsHandler.DisableAttachments();

            EnableBotSlices();
        }

        private void EnableBotSlices()
        {
            foreach (var part in _parts)
            {
                part.gameObject.SetActive(true);
                part.transform.parent = null;
                part.AddForce(Vector3.up * _force);
            }
        }
    }
}