using UnityEngine;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static void SetGameObjectActive(this Component component) =>
            component.gameObject.SetActive(true);

        public static void SetGameObjectInactive(this Component component) =>
            component.gameObject.SetActive(false);

        public static void SafeDebug(this Component component, object objectContent, LogType logType = LogType.Log)
        {
#if UNITY_EDITOR
            switch (logType)
            {
                case LogType.Log:
                    Debug.Log(objectContent);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(objectContent);
                    break;
                default:
                    Debug.LogError(objectContent);
                    break;
            }
#endif
        }
    }
} 