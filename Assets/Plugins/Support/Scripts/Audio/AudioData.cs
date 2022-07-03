using System;
using UnityEngine;

namespace Support.Audio
{
    [CreateAssetMenu(menuName = "Support/AudioData", fileName = "NewAudioData")]
    public sealed class AudioData : ScriptableObject
    {
        [SerializeField] private StringAudioClipDictionary audioClips;

        public AudioInfoContainer GetClip(string key)
        {
            if (audioClips.ContainsKey(key))
                return audioClips[key];
            
            TemplateUtils.SafeDebug($"Audio data does not contain \"{key}\"");
            return null;
        }
    }
    
    [Serializable]
    public sealed class StringAudioClipDictionary : SerializableDictionary<string, AudioInfoContainer> { }

    [Serializable]
    public sealed class AudioInfoContainer
    {
        public AudioClip audioClip;

        [Range(0, 256)] public int priority = 128;
        [Range(0, 1)] public float volume = 1;
        [Range(-3, 3)] public float pitch = 1;
        [Range(-1, 1)] public float sterioPan = 0;
        [Range(0, 1)] public float spatialBlend = 0;
        [Range(0, 1.1f)] public float reverbZoneMix = 1;
    }
}