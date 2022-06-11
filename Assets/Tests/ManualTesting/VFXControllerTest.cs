using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Support.Tests.Manual
{
    public class VFXControllerTest : MonoBehaviour
    {
        [SerializeField] private string volumeId;
        [SerializeField] private float lerpSpeed = 1;

        [Inject] private readonly VFXController _vfxController;
        
        [Button("Change volume without lerp")]
        public void ChangeVolumeWithOutLerp()
        {
            var randomWeight = Random.Range(0f, 1f);
            _vfxController.ChangePostProcessingVolume(volumeId, randomWeight);
        }
        
        [Button("Change volume with lerp")]
        public void ChangeVolumeWithLerp()
        {
            var randomWeight = Random.Range(0f, 1f);
            _vfxController.ChangePostProcessingVolume(volumeId, randomWeight, lerpSpeed);
        }
    }
}