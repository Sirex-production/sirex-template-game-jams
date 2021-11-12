using Extensions;
using UnityEngine;

namespace Support
{
    /// <summary>
    /// Class that implements singleton pattern and provides functionality of MonoBehaviour.
    /// </summary>
    /// <typeparam name="T">Type of class that will use singleton</typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>One single existing instance of particular class that implements MonoSingleton</summary>
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                this.SafeDebug($"There is more than one singleton of type {typeof(T)} in the scene", LogType.Warning);
                
                Destroy(this);
                return;
            }

            Instance = this as T;
        }
    }
}