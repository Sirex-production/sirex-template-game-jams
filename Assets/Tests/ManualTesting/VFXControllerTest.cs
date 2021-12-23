using NaughtyAttributes;
using UnityEngine;

namespace Support.Tests.Manual
{
    public class VFXControllerTest : MonoBehaviour
    {
        [SerializeField] private string volumeId;
        [SerializeField] private float lerpSpeed = 1;
        
        [Button("Change volume without lerp")]
        public void ChangeVolumeWithOutLerp()
        {
            var randomWeight = Random.Range(0f, 1f);
            VFXController.Instance.ChangePostProcessingVolume(volumeId, randomWeight);
        }
        
        [Button("Change volume with lerp")]
        public void ChangeVolumeWithLerp()
        {
            var randomWeight = Random.Range(0f, 1f);
            VFXController.Instance.ChangePostProcessingVolume(volumeId, randomWeight, lerpSpeed);
        }
    }
}