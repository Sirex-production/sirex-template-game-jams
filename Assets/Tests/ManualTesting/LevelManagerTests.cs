using Support;
using UnityEngine;

namespace Tests.Manual
{
    public class LevelManagerTests : MonoBehaviour
    {
        [SerializeField] private KeyCode keyToLoadNextLevel = KeyCode.Space;
        [SerializeField] private KeyCode keyToRestartLevel = KeyCode.R;
        
        private void Update()
        {
            if(Input.GetKeyUp(keyToLoadNextLevel))
                LevelManager.Instance.LoadNextLevel();
            
            if(Input.GetKeyUp(keyToRestartLevel))
                LevelManager.Instance.RestartLevel();
        }
    }
}