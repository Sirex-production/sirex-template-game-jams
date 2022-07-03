using System;
using System.Collections.Generic;
using System.Linq;
using Support.Extensions;
using UnityEngine;
using UnityEngine.Rendering;

namespace Support
{
    public class VFXController : MonoBehaviour
    {
        [SerializeField] private PostProcessingPair[] postProcessingPresets;

        private Dictionary<string, Volume> _postProcessingPresetsDictionary;
        private Dictionary<Volume, Coroutine> _volumeCoroutines = new();
        private void Awake()
        {
            _postProcessingPresetsDictionary = postProcessingPresets.ToDictionary(pair => pair.effectId, pair => pair.postProcessingVolume);
        }

        private void AddCoroutineToTheStackSafely(Volume key, Coroutine coroutine)
        {
            if (_volumeCoroutines.ContainsKey(key))
            {
                var volumeCoroutine = _volumeCoroutines[key]; 
                if(volumeCoroutine != null)
                    StopCoroutine(volumeCoroutine);
                
                _volumeCoroutines[key] = coroutine;
                
                return;
            }
            
            _volumeCoroutines.Add(key, coroutine);
        }

        /// <summary>
        /// Changes post particular volume weight.
        /// </summary>
        /// <param name="postProcessingVolumeName">Identifier of particular post processing volume</param>
        /// <param name="weight">Weight value that will be assigned to the volume</param>
        /// <param name="lerpSpeed">Speed for lerping between weights. By default equals to 0</param>
        public void ChangePostProcessingVolume(string postProcessingVolumeName, float weight, float lerpSpeed = 0)
        {
            if (_postProcessingPresetsDictionary.TryGetValue(postProcessingVolumeName, out Volume volume))
            {
                volume.enabled = true;

                if (lerpSpeed > 0)
                {
                    var lerpCoroutine = this.LerpCoroutine(lerpSpeed, volume.weight, weight, lerpValue => volume.weight = lerpValue);
                    AddCoroutineToTheStackSafely(volume, lerpCoroutine);
                }
                else
                {
                    if(_volumeCoroutines.ContainsKey(volume))
                        StopCoroutine(_volumeCoroutines[volume]);
                    
                    volume.weight = weight;
                }

                if (volume.weight < .0001f)
                    volume.enabled = false;
            }
        }
    }

    [Serializable]
    public class PostProcessingPair
    {
        public string effectId;
        public Volume postProcessingVolume;
    }
}