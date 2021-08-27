using UnityEngine;

namespace Support
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
#if UNITY_EDITOR
                Debug.LogWarning($"There is more than one singleton of type {typeof(T)} in the scene");
#endif
                Destroy(this);
                return;
            }

            Instance = this as T;
        }
    }
}