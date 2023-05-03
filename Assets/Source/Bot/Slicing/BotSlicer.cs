using System.Collections;
using UnityEngine;

namespace Source.Bot.Slicing
{
    [RequireComponent(typeof(BotDeathHandler), typeof(BotHealth))]
    public sealed class BotSlicer : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private float _force;
        [SerializeField] private float _timeToDestroy;

        private Vector3[] _partsPositions;
        private Quaternion[] _partsRotations;
        private BotHealth _botHealth;
        private BotDeathHandler _botDeathHandler;
        private Coroutine _coroutine;

        private void Awake()
        {
            _botHealth = GetComponent<BotHealth>();
            _botDeathHandler = GetComponent<BotDeathHandler>();
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

                _parts[i].transform.SetLocalPositionAndRotation(_partsPositions[i], _partsRotations[i]);
            }
        }

        private void InitParts()
        {
            _partsPositions = new Vector3[_parts.Length];
            _partsRotations = new Quaternion[_parts.Length];

            for (var i = 0; i < _parts.Length; i++)
            {
                _parts[i].gameObject.SetActive(false);

                _partsPositions[i] = _parts[i].transform.localPosition;
                _partsRotations[i] = _parts[i].transform.localRotation;
            }
        }

        private void OnDied()
        {
            if (_botHealth.enabled == false)
                return;

            _botDeathHandler.DisableBot();

            EnableBotSlices();

            StartWaitTimeToDisableSlices();
        }

        private void EnableBotSlices()
        {
            foreach (var part in _parts)
            {
                part.gameObject.SetActive(true);
                part.AddForce(Vector3.up * _force);
            }
        }

        private void StartWaitTimeToDisableSlices()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitTimeToDisableSlices());
        }

        private IEnumerator WaitTimeToDisableSlices()
        {
            yield return new WaitForSeconds(_timeToDestroy);

            gameObject.SetActive(false);
        }
    }
}