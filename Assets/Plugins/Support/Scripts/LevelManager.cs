using System;
using Support.SLS;
using UnityEngine.SceneManagement;

namespace Support
{
    /// <summary>
    /// Class that manages levels
    /// </summary>
    public class LevelManager : MonoSingleton<LevelManager>
    {
        /// <summary>
        /// Loads level
        /// </summary>
        /// <param name="levelNumber">Level index that will be loaded</param>
        /// <exception cref="ArgumentException"></exception>
        public void LoadLevel(int levelNumber)
        {
            if (levelNumber < 0)
                throw new ArgumentException($"There is no level with such index \"{levelNumber}\"");

            var sceneIndex = levelNumber < SceneManager.sceneCountInBuildSettings - 1
                ? levelNumber
                : levelNumber % SceneManager.sceneCountInBuildSettings;
            
            SceneManager.LoadScene(sceneIndex);
        }

        /// <summary>Restarts last level that was saved in progress(SaveLoadSystem)</summary>
        public void RestartLevel()
        {
            LoadLevel(SaveLoadSystem.Instance.SaveData.CurrentLevelNumber.Data);
        }
        
        /// <summary>Loads next level and modifies progress in SaveLoadSystem</summary>
        public void LoadNextLevel()
        {
            SaveLoadSystem.Instance.SaveData.CurrentLevelNumber.Data++;
            SaveLoadSystem.Instance.PerformSave();
            
            LoadLevel(SaveLoadSystem.Instance.SaveData.CurrentLevelNumber.Data);
        }
    }
}