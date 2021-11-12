using System;
using System.Collections;
using UnityEngine;

namespace Extensions
{
    /// <summary>
    /// Class that holds all extension methods for MonoBehaviour class 
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        private static IEnumerator WaitAndDoRoutine(float pause, Action action)
        {
            yield return new WaitForSeconds(pause);
            
            action?.Invoke();
        }

        private static IEnumerator DoAfterNextFrameRoutine(Action action)
        {
            yield return null;
            
            action?.Invoke();
        }

        private static IEnumerator RepeatRoutine(float pause, Action action, bool startWithPause)
        {
            var interval = new WaitForSeconds(pause);

            if (startWithPause)
                yield return interval;

            while (true)
            {
                action?.Invoke();
                
                yield return interval;
            }
        }

        private static IEnumerator LerpRoutine(float speed, float a, float b, Action<float> action)
        {
            if(action == null)
                yield break;
            
            speed = Mathf.Min(Mathf.Abs(b - a), Mathf.Abs(speed));
            speed = b < a ? -speed : speed;
            
            float currentValue = a;

            while (Math.Abs(currentValue - b) > .001f)
            {
                action(currentValue);
                currentValue += Time.deltaTime * speed;
                
                yield return null;
            }
            
            action(b);
        }

        /// <summary>
        /// Starts coroutine that repeats function with some pause
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <param name="pause">Pause between invoking function</param>
        /// <param name="action">Function that will be invoked</param>
        /// <param name="startWithPause">Defines weather coroutine makes pause before invoking function for the first time</param>
        /// <returns>Returns started coroutine</returns>
        public static Coroutine RepeatCoroutine(this MonoBehaviour monoBehaviour, float pause, Action action, bool startWithPause = false)
        {
            return monoBehaviour.StartCoroutine(RepeatRoutine(pause, action, startWithPause));
        }

        /// <summary>
        /// Invokes method on the next frame
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <param name="action">Function that will be invoked</param>
        /// <returns>Returns started coroutine</returns>
        public static Coroutine DoAfterNextFrameCoroutine(this MonoBehaviour monoBehaviour, Action action)
        {
            return monoBehaviour.StartCoroutine(DoAfterNextFrameRoutine(action));
        }

        /// <summary>
        /// Invokes function after some time
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <param name="pause">Pause after function will be invoked</param>
        /// <param name="action">Function that will be invoked</param>
        /// <returns>Returns started coroutine</returns>
        public static Coroutine WaitAndDoCoroutine(this MonoBehaviour monoBehaviour, float pause, Action action)
        {
            return monoBehaviour.StartCoroutine(WaitAndDoRoutine(pause, action));
        }

        /// <summary>
        /// Lerps between two values with given speed
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <param name="speed">Lerping speed</param>
        /// <param name="a">Initial value</param>
        /// <param name="b">Target value</param>
        /// <param name="action">Function that will be invoked on each frame. Takes float that represents lerping value at a given moment of time</param>
        /// <returns></returns>
        public static Coroutine LerpCoroutine(this MonoBehaviour monoBehaviour, float speed, float a, float b, Action<float> action)
        {
            return monoBehaviour.StartCoroutine(LerpRoutine(speed, a, b, action));
        }
    }
}