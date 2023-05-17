using System.Collections;
using UnityEngine;

namespace Source.GameLogic
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class RandomSoundsGenerator : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _sounds;
        [SerializeField] private float _delay;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(WaitTimeToSetNewSound(_delay));
        }

        private void PlayRandomSound()
        {
            var randomSoundNumber = Random.Range(0, _sounds.Length);
            
            _audioSource.PlayOneShot(_sounds[randomSoundNumber]);
            StartCoroutine(WaitTimeToSetNewSound(_delay + _sounds[randomSoundNumber].length));
        }

        private IEnumerator WaitTimeToSetNewSound(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            PlayRandomSound();
        }
    }
}