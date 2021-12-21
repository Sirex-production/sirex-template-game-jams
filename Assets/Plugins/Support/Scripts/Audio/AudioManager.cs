using Support;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
namespace Support { 
    public class AudioManager : MonoSingleton<AudioManager>
    {
        //getting resources
        [Foldout("Source")]
        [SerializeField]
        [ReorderableList]
        private Sound[] sounds;

        [Foldout("Source")]
        [SerializeField]
        [ReorderableList]

        private Sound[] music;
        [Foldout("Source")]
        [SerializeField]
        [ReorderableList]
        private Sound[] dialogs;
        //Dictionary - fetch resources from Arrays to simplisify choosing a sound
        private Dictionary<string,Sound> _sounds = new Dictionary<string, Sound>();
        private Dictionary<string,Sound> _music = new Dictionary<string, Sound>();
        private Dictionary<string,Sound> _dialogs = new Dictionary<string, Sound>();

        private void Awake()
        {
            CopySoundsListIntoDictionary(dialogs, _dialogs);
            CopySoundsListIntoDictionary(music,_music );
            CopySoundsListIntoDictionary(sounds,_sounds );
        }

        #region PRIVATE_METHODS
        private void CopySoundsListIntoDictionary(Sound[] arr, Dictionary<string, Sound> dic)
        {
            foreach(Sound s in arr)
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
        
        private Sound GetRawSound(Dictionary<string, Sound> arr,string s)
        {
            Sound result;
            if (!arr.TryGetValue(s,out result))
                return null;
            return result;
        }

        private void PlayRawSound(Dictionary<string, Sound> arr, string s) {
            var res = GetRawSound(arr, s);
            res?.Source.Play();
        }
        private void StopRawSound(Dictionary<string, Sound> arr, string s)
        {
            var res = GetRawSound(arr, s);
            res?.Source.Stop();
        }
        private void ResetRawSound(Dictionary<string, Sound> arr, string s)
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
            PlayRawSound(_music, s);
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
            StopRawSound(_music, s);
        }
        public void StopSound(string s)
        {
            StopRawSound(_music, s);
        }
        public void StopDialog(string s)
        {
            StopRawSound(_music, s);
        }
        #endregion
        #region RESET_SOUND
        public void ResetMusic(string s)
        {
            ResetRawSound(_music, s);
        }
        public void ResetSound(string s)
        {
            ResetRawSound(_music, s);
        }
        public void ResetDialog(string s)
        {
            ResetRawSound(_music, s);
        }
        #endregion
        #endregion
    }
}
