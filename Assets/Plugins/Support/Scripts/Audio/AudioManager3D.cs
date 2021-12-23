using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Support
{
    
    public class AudioManager3D : MonoSingleton<AudioManager3D>
    {

        //Dictionary - fetch resources from Arrays to simplisify choosing a sound
     
        private Dictionary<string, Sound2D> _sounds = new Dictionary<string, Sound2D>();

        private Dictionary<string, Sound2D> _musics = new Dictionary<string, Sound2D>();
  
        private Dictionary<string, Sound2D> _dialogs = new Dictionary<string, Sound2D>();

        public void AddSound(Sound3D s)
        {
            s.Sound.Source.clip = s.Sound.Clip;
            s.Sound.Source.pitch = s.Sound.Pitch;
            s.Sound.Source.volume = s.Sound.Volume;
            s.Sound.Source.loop = s.Sound.Loop;
            s.Sound.Source.playOnAwake = false;
            s.Sound.Source.spatialBlend = s.SpatialBlend;

            switch (s.Type)
            {
                case Sound3D.TypeOfSound.SoundEffect:
                    {
                        _sounds.Add(s.Sound.Name, s.Sound);
                        break;
                    }
                case Sound3D.TypeOfSound.Dialog:
                    {
                        _dialogs.Add(s.Sound.Name, s.Sound);
                        break;
                    }
                case Sound3D.TypeOfSound.Music:
                    {
                        _musics.Add(s.Sound.Name, s.Sound);
                        break;
                    }
                default:
                    break;
            }
        }


        #region PRIVATE_METHODS

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
