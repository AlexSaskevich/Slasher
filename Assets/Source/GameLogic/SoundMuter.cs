using System.Collections.Generic;
using UnityEngine;

namespace Source.GameLogic
{
    [RequireComponent(typeof(AudioListener))]
    public sealed class SoundMuter : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _audioSources;
        
        private static readonly List<float> Volumes = new();

        private static AudioSource[] s_audioSources;
        
        private AudioListener _audioListener;

        public static bool IsMuted { get; private set; }

        private void Awake()
        {
            _audioListener = GetComponent<AudioListener>();

            s_audioSources = _audioSources;

            foreach (var audioSource in s_audioSources)
                Volumes.Add(audioSource.volume);
        }

        private void Start()
        {
            _audioListener.enabled = !IsMuted;
            
            if(IsMuted)
            {
                foreach (var audioSource in s_audioSources)
                    audioSource.volume = 0;
            }
            else
            {
                for (var i = 0; i < s_audioSources.Length; i++)
                    s_audioSources[i].volume = Volumes[i];
            }   
        }

        public static void Mute()
        {
            IsMuted = true;

            foreach (var audioSource in s_audioSources)
                audioSource.volume = 0;
        }

        public static void Unmute()
        {
            IsMuted = false;

            for (var i = 0; i < s_audioSources.Length; i++)
                s_audioSources[i].volume = Volumes[i];
        }
    }
}