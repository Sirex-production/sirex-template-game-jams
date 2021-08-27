using UnityEngine;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static void SetGameObjectActive(this Component component) =>
            component.gameObject.SetActive(true);

        public static void SetGameObjectInactive(this Component component) =>
            component.gameObject.SetActive(false);

        public static void SafeDebug(this Component component, string content, LogType logType = LogType.Log)
        {
#if UNITY_EDITOR
            switch (logType)
            {
                case LogType.Log:
                    Debug.Log(content);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(content);
                    break;
                default:
                    Debug.LogError(content);
                    break;
            }
#endif
        }
    }
} 