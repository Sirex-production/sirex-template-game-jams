using System;
using UnityEngine.SceneManagement;

namespace Support
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public void LoadLevel(int levelNumber)
        {
            if (levelNumber < 0)
                throw new ArgumentException($"There is no level with such index \"{levelNumber}\"");

            var sceneIndex = levelNumber < SceneManager.sceneCountInBuildSettings - 1
                ? levelNumber
                : levelNumber % SceneManager.sceneCountInBuildSettings;
            
            SceneManager.LoadScene(sceneIndex);
        }

        public void RestartLevel()
        {
            LoadLevel(SaveLoadSystem.Instance.SaveData.currentLevel);
        }
        
        public void LoadNextLevel()
        {
            SaveLoadSystem.Instance.SaveData.currentLevel++;
            LoadLevel(SaveLoadSystem.Instance.SaveData.currentLevel);
        }
    }
}