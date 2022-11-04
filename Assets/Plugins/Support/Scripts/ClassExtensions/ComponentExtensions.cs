using Support.Utilities;
using UnityEngine;

namespace Support.Extensions
{
    /// <summary>
    /// Class that holds all extension methods for Component class 
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Turns on the GameObject to which component is attached
        /// </summary>
        /// <param name="component"></param>
        public static void SetGameObjectActive(this Component component) =>
            component.gameObject.SetActive(true);

        /// <summary>
        /// Turns off the GameObject to which component is attached
        /// </summary>
        /// <param name="component"></param>
        public static void SetGameObjectInactive(this Component component) =>
            component.gameObject.SetActive(false);

        /// <summary>
        /// Prints log to unity console. During build runtime suppresses all of the messages thereby increases performance
        /// </summary>
        /// <param name="component"></param>
        /// <param name="objectContent">Object that will be logged</param>
        /// <param name="logType">Type of log in the console</param>
        public static void SafeDebug(this Component component, object objectContent, LogType logType = LogType.Log)
        {
#if UNITY_EDITOR
            TemplateUtils.SafeDebug(objectContent, logType);
#endif
        }

        /// <summary>
        /// Assigns one layer to all children
        /// </summary>
        /// <param name="layer">Layer that will be assigned to the children</param>
        public static void SetLayerToAllChildren(this Component component, int layer)
        {
            var childrenTransforms = component.GetComponentsInChildren<Transform>();
            
            foreach (var childrenTransform in childrenTransforms) 
                childrenTransform.gameObject.layer = layer;
            
        }
    }
}