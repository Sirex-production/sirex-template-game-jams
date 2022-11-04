using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Support.LevelManagement
{
    /// <summary>
    /// Class that manages levels
    /// </summary>
    public sealed class LevelService : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float delayBeforeLoadingScene = .1f;
        [SerializeField] [Min(0)] private float delayAfterSceneIsLoaded = .1f;

        public event Action OnSceneLoadingStarted;
        public event Action<float> OnSceneLoadingProgressChanged;
        public event Action OnSceneLoadingFinished;

        /// <summary>
        /// Loads level
        /// </summary>
        /// <param name="levelNumber">Level index that will be loaded</param>
        /// <exception cref="ArgumentException"></exception>
        public void LoadScene(int sceneIndex)
        {
            if (sceneIndex < 0)
                throw new ArgumentException($"There is no level with such index \"{sceneIndex}\"");

            StartCoroutine(LoadLevelRoutine(sceneIndex));
        }

        /// <summary>Restarts last level that was saved in progress(SaveLoadSystem)</summary>
        public void RestartLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            LoadScene(currentSceneIndex);
        }

        private IEnumerator LoadLevelRoutine(int sceneIndex)
        {
            if(delayBeforeLoadingScene > 0f)
                yield return new WaitForSeconds(delayBeforeLoadingScene);
            
            OnSceneLoadingStarted?.Invoke();
            
            var loadSceneAsyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!loadSceneAsyncOperation.isDone)
            {
                OnSceneLoadingProgressChanged?.Invoke(loadSceneAsyncOperation.progress);

                yield return null;
            }
            
            if(delayAfterSceneIsLoaded > 0f)
                yield return new WaitForSeconds(delayAfterSceneIsLoaded);
            
            OnSceneLoadingFinished?.Invoke();
        }
    }
}