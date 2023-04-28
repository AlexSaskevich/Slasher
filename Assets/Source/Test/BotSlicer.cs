using Source.Bot;
using UnityEngine;

namespace Source.Test
{
    public class BotSlicer : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _parts;
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private BotHumanHealth _botHumanHealth;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private float _force;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _botHumanHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _botHumanHealth.Died -= OnDied;
        }

        private void Start()
        {
            for (int i = 0; i < _parts.Length; i++)
                _parts[i].gameObject.SetActive(false);
        }

        private void OnDied()
        {
            _skinnedMeshRenderer.enabled = false;
            _animator.enabled = false;

            DisableColliders();

            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].gameObject.SetActive(true);
                _parts[i].AddForce(Vector3.up * _force);
            }
        }

        private void DisableColliders()
        {
            for (int i = 0; i < _colliders.Length; i++)
                _colliders[i].enabled = false;
        }
    }
}