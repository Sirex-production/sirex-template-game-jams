using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Support.Extensions
{
    public static class TMPExtensions
    {
        private static IEnumerator SpawnTextRoutine(TMP_Text textArea, string textToDisplay, float spawnDelayTime, Action onComplete)
        {
            if (String.IsNullOrEmpty(textToDisplay))
            {
                onComplete?.Invoke();
                yield break;
            }

            spawnDelayTime = Mathf.Abs(spawnDelayTime);

            var letters = textToDisplay.ToCharArray();
            var waitForDelayInSeconds = new WaitForSeconds(spawnDelayTime);
            textArea.text = "";

            yield return waitForDelayInSeconds;

            bool isTag = false;
            var tag = "";
            
            foreach (var letter in letters)
            {
                if (letter == '<')
                    isTag = true;
                else if (letter == '>')
                {
                    isTag = false;
                    textArea.text += tag;
                    tag = "";
                }

                if (isTag)
                {
                    tag += letter;
                    continue;
                }
            
                textArea.text += letter;
                yield return waitForDelayInSeconds;
            }
            
            onComplete?.Invoke();
        }
        
        /// <summary>
        /// Display content in certain text area with writing effect
        /// </summary>
        /// <param name="textArea">Area where content will be displayed</param>
        /// <param name="textToDisplay">Content that text area will display</param>
        /// <param name="spawnDelayTime">Pause between appearing letters</param>
        /// <returns>Coroutine that spawns text</returns>
        public static Coroutine SpawnTextCoroutine(this TMP_Text textArea, string textToDisplay, float spawnDelayTime, Action onComplete = null)
        {
            return textArea.StartCoroutine(SpawnTextRoutine(textArea, textToDisplay, spawnDelayTime, onComplete));
        }
    }
}