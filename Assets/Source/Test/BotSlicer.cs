using Source.GameLogic;
using UnityEngine;

namespace Source.Test
{
    public class BotSlicer : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private float _force;

        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Health _botHealth;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _botHealth = GetComponent<Health>();
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
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
            _skinnedMeshRenderer.enabled = false;
            _animator.enabled = false;

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
    }
}