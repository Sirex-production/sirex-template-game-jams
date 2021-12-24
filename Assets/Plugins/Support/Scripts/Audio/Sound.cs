using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

namespace Support
{
    [Serializable]
     public class Sound 
    {

        [SerializeField]
        private string name;
        [SerializeField]
        private AudioClip clip;

        [SerializeField]
        [Foldout("Settings")]
        [Range(0, 1)]
        private float volume;
        [SerializeField]
        [Foldout("Settings")]
        [Range(0, 3)]
        private float pitch;

        [SerializeField]
        private bool loop = false;
        public string Name => name;
        public AudioClip Clip => clip;
        public float Volume => volume;
        public float Pitch => pitch;
        public bool Loop => loop;
 
    }
    public enum TypeOfSound
    {
        Music,
        SoundEffect,
        Dialog
    }
    [Serializable]
    public class AudioContainer
    {
        [SerializeField]
        private TypeOfSound type;
        [SerializeField]
        private Sound[] sounds;

        public TypeOfSound Type => type;
        public Sound[] Sounds => sounds;
    }
}
