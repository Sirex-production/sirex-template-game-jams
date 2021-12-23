using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

namespace Support
{
    [Serializable]
    [RequireComponent(typeof(AudioSource))]
    public class Sound2D
    {
        #region SERIALIZE
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
        #endregion
        #region PUBLIC
        public string Name => name;
        public AudioClip Clip => clip;
        public float Volume => volume;
        public float Pitch => pitch;
        public bool Loop => loop;


        [HideInInspector]
        public AudioSource Source;
        #endregion
    }
}
