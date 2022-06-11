using UnityEngine;
using Zenject;

namespace Support.Tests.Manual
{
    public class LevelManagerTests : MonoBehaviour
    {
        [SerializeField] private KeyCode keyToLoadNextLevel = KeyCode.Space;
        [SerializeField] private KeyCode keyToRestartLevel = KeyCode.R;

        [Inject] private readonly LevelManager _levelManager;
        
        private void Update()
        {
            if(Input.GetKeyUp(keyToLoadNextLevel))
                _levelManager.LoadNextLevel();
            
            if(Input.GetKeyUp(keyToRestartLevel))
                _levelManager.RestartLevel();
        }
    }
}