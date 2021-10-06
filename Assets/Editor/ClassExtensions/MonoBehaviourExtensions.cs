using System;
using System.Collections;
using UnityEngine;

namespace Extensions
{
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

        public static Coroutine RepeatCoroutine(this MonoBehaviour monoBehaviour, float pause, Action action, bool startWithPause = false)
        {
            return monoBehaviour.StartCoroutine(RepeatRoutine(pause, action, startWithPause));
        }

        public static Coroutine DoAfterNextFrameCoroutine(this MonoBehaviour monoBehaviour, Action action)
        {
            return monoBehaviour.StartCoroutine(DoAfterNextFrameRoutine(action));
        }

        public static Coroutine WaitAndDoCoroutine(this MonoBehaviour monoBehaviour, float pause, Action action)
        {
            return monoBehaviour.StartCoroutine(WaitAndDoRoutine(pause, action));
        }

        public static Coroutine LerpCoroutine(this MonoBehaviour monoBehaviour, float speed, float a, float b, Action<float> action)
        {
            return monoBehaviour.StartCoroutine(LerpRoutine(speed, a, b, action));
        }
    }
}