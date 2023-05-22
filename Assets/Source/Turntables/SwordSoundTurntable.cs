using UnityEngine;

namespace Source.Turntables
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class SwordSoundTurntable : MonoBehaviour
    {
        [SerializeField] private AudioClip _swordSound;
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;
        [SerializeField] private float _volume;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySwordSound()
        {
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.volume = _volume;
            _audioSource.PlayOneShot(_swordSound);
        }
    }
}