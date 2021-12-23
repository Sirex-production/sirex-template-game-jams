using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Support {
    [RequireComponent(typeof(AudioSource))]
    public class Sound3D : MonoBehaviour
    {
        [SerializeField]
        private Sound2D sound;
        [SerializeField]
        private TypeOfSound type;
        [SerializeField]
        private float spatialBlend;
        public enum TypeOfSound
        {
            Music,
            SoundEffect,
            Dialog
        }
        public Sound2D Sound => sound;
        public TypeOfSound Type => type;
        public float SpatialBlend => spatialBlend;

        private void Awake()
        {
            sound.Source = this.GetComponent<AudioSource>();

        }

        private void OnEnable()
        {
            AudioManager3D.Instance.AddSound(this); 
        }
    }
}