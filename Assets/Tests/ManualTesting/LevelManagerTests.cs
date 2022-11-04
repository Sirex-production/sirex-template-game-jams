using Support.LevelManagement;
using UnityEngine;
using Zenject;

namespace Support.Tests.Manual
{
    public class LevelManagerTests : MonoBehaviour
    {
        [SerializeField] private KeyCode keyToRestartLevel = KeyCode.R;

        [Inject] private readonly LevelService _levelManager;
        
        private void Update()
        {
            if(Input.GetKeyUp(keyToRestartLevel))
                _levelManager.RestartLevel();
        }
    }
}