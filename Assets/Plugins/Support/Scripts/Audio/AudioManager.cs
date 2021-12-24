using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaughtyAttributes;
namespace Support { 
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField]
        private AudioWrapper[] sounds;
        private Dictionary<TypeOfSound,Dictionary<string,AudioPair>> _dic = new Dictionary<TypeOfSound, Dictionary<string, AudioPair>>();

        private void Start()
        {
            
            foreach (var type in Enum.GetValues(typeof(TypeOfSound)))
            {

                _dic.Add(((TypeOfSound)type) , new Dictionary<string, AudioPair>());   
            }
            foreach (var s in sounds)
            {
                foreach (var wrap in s.Audios)
                {
                    foreach (var audio in wrap.Sounds)
                    {
                        _dic[wrap.Type].Add(audio.Name, new AudioPair(s,audio));
                    }
                }
            }
        }
        public void PlaySound(TypeOfSound type, string name)
        {
            var res = _dic[type][name];
            res.Wrapper.Play(res.Sound);
        }

        public void StopSound(TypeOfSound type, string name)
        {
            var res = _dic[type][name];
            res.Wrapper.Stop(res.Sound);
        }
    }
    public class AudioPair
    {
        private AudioWrapper _wrapper;
        private Sound _sound;

        public AudioWrapper Wrapper => _wrapper;
        public Sound Sound => _sound;
       public AudioPair(AudioWrapper wrapper, Sound sound)
        {
            _wrapper = wrapper;
            _sound = sound;
        }
       
    }
}
