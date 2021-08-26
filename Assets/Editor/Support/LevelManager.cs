using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Support
{
    public class LevelManager : MonoBehaviour
    {
        private void Start()
        {
            GameController.Instance.OnLevelLoad += LoadScene;
        }

        private void OnDestroy()
        {
            GameController.Instance.OnLevelLoad -= LoadScene;
        }
        
        private void LoadScene(int levelNumber)
        {
            if (levelNumber < 0)
                throw new ArgumentException($"There is no level with such index \"{levelNumber}\"");

            var sceneIndex = levelNumber < SceneManager.sceneCountInBuildSettings - 1
                ? levelNumber
                : levelNumber % SceneManager.sceneCountInBuildSettings;
            
            SceneManager.LoadScene(sceneIndex);
        }
    }
}