using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
namespace Support
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioWrapper : MonoBehaviour
    {
        [SerializeField]
        private List<AudioContainer> _audios;
        private AudioSource _source;
        private Dictionary<Sound,AudioSource> _dicSources= new Dictionary<Sound,AudioSource>();
        
        public List<AudioContainer> Audios => _audios;
        private void RemoveItemFromDict(Sound s)
        {
            AudioSource res;
            bool cond = _dicSources.TryGetValue(s, out res);
            if (!cond)
            {
                return;
            }
            res.Stop();
            _dicSources.Remove(s);
            if (_dicSources.Count == 0)
            {
                _source = res;
            }
            else
            {
                Destroy(res);
            }
        }
        public void Play(Sound s)
        {
            if(_source == null)
            {
                _source = gameObject.GetComponent<AudioSource>();
            }

            //avoiding repeating the same sound from one object
            AudioSource src;
            var cond = _dicSources.TryGetValue(s, out src);
            if (cond)
            {
                return;
            }
            //checking unoccupied sources
            if (!_source.isPlaying)
            {
                src = _source;  
            }
            else
            {
                src = gameObject.AddComponent<AudioSource>();
            }

            src.volume = s.Volume;
            src.clip = s.Clip;
            src.loop = s.Loop;
            src.pitch = s.Pitch;
            _dicSources.Add(s, src);
            src.Play();

            this.WaitAndDoCoroutine(src.clip.length,()=> RemoveItemFromDict(s));
        }
        public void Stop(Sound s)
        {
            RemoveItemFromDict(s);
        }
       
    }
}