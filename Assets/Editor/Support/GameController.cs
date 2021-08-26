using System;
using UnityEngine.SceneManagement;

namespace Support
{
    public class GameController : MonoSingleton<GameController>
    {
        public event Action<int> OnLevelLoad;
        public event Action<bool> OnLevelEnded;
        
        public void EndLevel(bool isVictory)
        {
            OnLevelEnded?.Invoke(isVictory);
        }

        public void LoadLevel(int levelIndex)
        {
            if(levelIndex != SceneManager.GetActiveScene().buildIndex)
                OnLevelLoad?.Invoke(levelIndex);
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