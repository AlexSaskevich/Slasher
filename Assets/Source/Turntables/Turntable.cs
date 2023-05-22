using UnityEngine;

namespace Source.Turntables
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Turntable : MonoBehaviour
    {
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;
        [SerializeField] private float _volume;
        [SerializeField] private AudioClip _sound;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        protected void SetRandomPitch()
        {
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        }

        protected void SetVolume()
        {
            _audioSource.volume = _volume;
        }

        protected void PlaySound()
        {
            _audioSource.PlayOneShot(_sound);
        }
    }
}