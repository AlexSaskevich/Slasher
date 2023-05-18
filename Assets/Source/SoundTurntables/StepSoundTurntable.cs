using UnityEngine;

namespace Source.SoundTurntables
{
    public sealed class StepSoundTurntable : MonoBehaviour
    {
        private const float TimeToStep = 0.5f;
        private const float MinPitch = 0.9f;
        private const float MaxPitch = 1.1f;
        
        private AudioSource _audioSource;
        private float _stepTimer;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayStepAudioClip()
        {
            _stepTimer += Time.deltaTime;

            if (_stepTimer < TimeToStep)
                return;
            
            _audioSource.Play();
            _audioSource.pitch = Random.Range(MinPitch, MaxPitch);
            _stepTimer = 0;
        }

        public void StopStepsSound()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }
    }
}