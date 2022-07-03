using System.Collections.Generic;
using NaughtyAttributes;
using Support.Extensions;
using UnityEngine;

namespace Support.Audio
{
    public sealed class AudioController : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int initialAmountOfAudioSources = 10;
        [Required]
        [SerializeField] private AudioData audioData;
        [SerializeField] private GameObject audioSourcesContainer;

        private List<AudioSourceRuntimeProperty> _audioSourcesRuntimeProperties;

        private void Awake()
        {
            if (audioSourcesContainer == null)
                audioSourcesContainer = gameObject;
            
            _audioSourcesRuntimeProperties = new List<AudioSourceRuntimeProperty>(initialAmountOfAudioSources);

            for (var i = 0; i < initialAmountOfAudioSources; i++)
                CreateNewAudioSource();
        }

        private AudioSourceRuntimeProperty CreateNewAudioSource()
        {
            var audioSourceAndItsClipKey = new AudioSourceRuntimeProperty
            {
                audioSource = audioSourcesContainer.AddComponent<AudioSource>(),
                audioClipKey = null
            };
                
            _audioSourcesRuntimeProperties.Add(audioSourceAndItsClipKey);

            return audioSourceAndItsClipKey;
        }

        private AudioSourceRuntimeProperty GetConfiguredAudioSource(string key)
        {
            var audioInfoContainer = audioData.GetClip(key);
            
            if (audioInfoContainer == null)
                return null;
            
            var audioSourceRuntimeData = _audioSourcesRuntimeProperties.SafeFirst(audioSource => !audioSource.audioSource.isPlaying) ?? CreateNewAudioSource();
            var freeAudioSource = audioSourceRuntimeData.audioSource;

            audioSourceRuntimeData.audioClipKey = key;
            freeAudioSource.clip = audioInfoContainer.audioClip;
            freeAudioSource.priority = audioInfoContainer.priority;
            freeAudioSource.volume = audioInfoContainer.volume;
            freeAudioSource.pitch = audioInfoContainer.pitch;
            freeAudioSource.panStereo = audioInfoContainer.sterioPan;
            freeAudioSource.spatialBlend = audioInfoContainer.spatialBlend;
            freeAudioSource.reverbZoneMix = audioInfoContainer.reverbZoneMix;

            return audioSourceRuntimeData;
        }

        /// <summary>
        /// Plays audio that corresponds to the given key
        /// </summary>
        /// <param name="key">Key that indicates audio clip</param>
        public void PlayAudio(string key)
        {
            var configuredAudioSource = GetConfiguredAudioSource(key);
            if (configuredAudioSource == null)
                return;
            
            GetConfiguredAudioSource(key).audioSource.Play();
        }

        /// <summary>
        /// Stops all of the audio sources with given key
        /// </summary>
        /// <param name="key">Key that indicates audio clip</param>
        public void StopAllWithGivenKey(string key)
        {
            foreach (var audioSourceRuntimeProperty in _audioSourcesRuntimeProperties)
            {
                if(audioSourceRuntimeProperty == null)
                    continue;
                
                if (audioSourceRuntimeProperty.audioClipKey == key)
                {
                    audioSourceRuntimeProperty.audioClipKey = null;
                    audioSourceRuntimeProperty.audioSource.Stop();
                }
            }
        }

        /// <summary>
        /// Stops all audio sources
        /// </summary>
        public void StopAll()
        {
            foreach (var audioSourceRuntimeProperty in _audioSourcesRuntimeProperties)
            {
                if(audioSourceRuntimeProperty == null)
                    continue;

                audioSourceRuntimeProperty.audioClipKey = null;
                audioSourceRuntimeProperty.audioSource.Stop();
            }
        }
    }
    
    internal sealed class AudioSourceRuntimeProperty
    {
        public AudioSource audioSource;
        public string audioClipKey;
    }
}