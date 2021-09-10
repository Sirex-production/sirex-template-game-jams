using UnityEngine;

namespace Support
{
    public static class TemplateUtils
    {
        public static void SafeDebug(object objectContent, LogType logType = LogType.Log)
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