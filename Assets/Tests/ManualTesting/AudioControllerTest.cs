using NaughtyAttributes;
using Support.Audio;
using UnityEngine;
using Zenject;

namespace Support.Tests.Manual
{
    public sealed class AudioControllerTest : MonoBehaviour
    {
        [SerializeField] private string audioClipKeyToPlay;

        [Inject] private AudioController _audioController;
        
        [Button]
        public void PlayAudio()
        {
            _audioController.PlayAudio(audioClipKeyToPlay);
        }
    }
}