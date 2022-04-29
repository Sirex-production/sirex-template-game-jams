using System;
using Support.SLS;
using UnityEngine.SceneManagement;
using Zenject;

namespace Support
{
    /// <summary>
    /// Class that manages levels
    /// </summary>
    public sealed class LevelManager
    {
        [Inject] private SaveLoadSystem _saveLoadSystem;
        
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
            LoadLevel(_saveLoadSystem.SaveData.CurrentLevelNumber.Value);
        }
        
        /// <summary>Loads next level and modifies progress in SaveLoadSystem</summary>
        public void LoadNextLevel()
        {
            _saveLoadSystem.SaveData.CurrentLevelNumber.Value++;
            _saveLoadSystem.PerformSave();
            
            LoadLevel(_saveLoadSystem.SaveData.CurrentLevelNumber.Value);
        }
    }
}