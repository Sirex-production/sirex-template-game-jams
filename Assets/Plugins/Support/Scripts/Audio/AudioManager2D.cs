using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;

namespace Support { 
    public class AudioManager2D : MonoSingleton<AudioManager2D>
    {
        //getting resources
        [Foldout("Source")]
        [SerializeField]
        [ReorderableList]
        private Sound2D[] sounds;

        [Foldout("Source")]
        [SerializeField]
        [ReorderableList]

        private Sound2D[] music;
        [Foldout("Source")]
        [SerializeField]
        [ReorderableList]
        private Sound2D[] dialogs;
        //Dictionary - fetch resources from Arrays to simplisify choosing a sound
        private Dictionary<string, Sound2D> _sounds = new Dictionary<string, Sound2D>();
        private Dictionary<string, Sound2D> _musics = new Dictionary<string, Sound2D>();
        private Dictionary<string, Sound2D> _dialogs = new Dictionary<string, Sound2D>();

        private void Awake()
        {
            base.Awake();
            CopySoundsListIntoDictionary(dialogs, _dialogs);
            CopySoundsListIntoDictionary(music, _musics);
            CopySoundsListIntoDictionary(sounds, _sounds);
        }

        #region PRIVATE_METHODS
        private void CopySoundsListIntoDictionary(Sound2D[] arr, Dictionary<string, Sound2D> dic)
        {
            foreach (Sound2D s in arr)
            {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.Clip;
                s.Source.pitch = s.Pitch;
                s.Source.volume = s.Volume;
                s.Source.loop = s.Loop;
                s.Source.playOnAwake = false;
                dic.Add(s.Name, s);
            }
        }

        protected Sound2D GetRawSound(Dictionary<string, Sound2D> arr, string s)
        {
            Sound2D result;
            if (!arr.TryGetValue(s, out result))
                return null;
            return result;
        }

        protected void PlayRawSound(Dictionary<string, Sound2D> arr, string s)
        {
            var res = GetRawSound(arr, s);
            res?.Source.Play();
        }
        protected void StopRawSound(Dictionary<string, Sound2D> arr, string s)
        {
            var res = GetRawSound(arr, s);
            res?.Source.Stop();
        }
        protected void ResetRawSound(Dictionary<string, Sound2D> arr, string s)
        {
            var res = GetRawSound(arr, s);
            if (res == null)
                return;
            res.Source.time = 0;
        }

        #endregion
        #region PUBLIC_METHODS
        #region PLAY_SOUND
        public void PlayMusic(string s)
        {
            PlayRawSound(_musics, s);
        }
        public void PlayDialog(string s)
        {
            PlayRawSound(_dialogs, s);
        }

        public void PlaySound(string s)
        {
            PlayRawSound(_sounds, s);
        }
        #endregion
        #region STOP_SOUND
        public void StopMusic(string s)
        {
            StopRawSound(_musics, s);
        }
        public void StopSound(string s)
        {
            StopRawSound(_sounds, s);
        }
        public void StopDialog(string s)
        {
            StopRawSound(_dialogs, s);
        }
        #endregion
        #region RESET_SOUND
        public void ResetMusic(string s)
        {
            ResetRawSound(_musics, s);
        }
        public void ResetSound(string s)
        {
            ResetRawSound(_sounds, s);
        }
        public void ResetDialog(string s)
        {
            ResetRawSound(_dialogs, s);
        }
        #endregion
        #endregion
    
    }
}
