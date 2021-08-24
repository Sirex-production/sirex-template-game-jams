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

        private void LoadScene(int sceneIndex)
        {
            if (SceneManager.sceneCountInBuildSettings - 1 <= 0)
                throw new SystemException("There is no scenes in build settings");

            var lastSceneIndexInBuildSettings = SceneManager.sceneCountInBuildSettings - 1;

            var sceneIndexToLoad = sceneIndex % lastSceneIndexInBuildSettings == 0 ?
                lastSceneIndexInBuildSettings : 
                sceneIndex % lastSceneIndexInBuildSettings;

            SceneManager.LoadScene(sceneIndexToLoad);
        }
    }
}