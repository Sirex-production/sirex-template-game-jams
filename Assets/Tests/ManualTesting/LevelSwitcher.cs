using Support;
using UnityEngine;

namespace Tests.Manual
{
    public class LevelSwitcher : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Space))
                GameController.Instance.LoadNextLevel();
        }
    }
}