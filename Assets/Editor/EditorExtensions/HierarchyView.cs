using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{ 
    [InitializeOnLoad]
    public class HierarchyView : MonoBehaviour
    {
        static HierarchyView()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= DrawCustomHierarchyGui;
            EditorApplication.hierarchyWindowItemOnGUI += DrawCustomHierarchyGui;
        }

        private static void DrawCustomHierarchyGui(int instanceId, Rect hierarchyRect)
        {
            GameObject selectedObject = null;
            HierarchyHighlighter hierarchyHighlighter = null;
            Color backgroundColor;
            Color fontColor;
            Font fontStyleInHierarchy;

            selectedObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (selectedObject != null)
                hierarchyHighlighter = selectedObject.GetComponent<HierarchyHighlighter>();

            if (hierarchyHighlighter == null)
                return;

            backgroundColor = hierarchyHighlighter.BackgroundColor;
            fontColor = hierarchyHighlighter.FontColor;
            fontStyleInHierarchy = hierarchyHighlighter.FontStyle;

            if (backgroundColor.a > 0f)
                EditorGUI.DrawRect(hierarchyRect, backgroundColor);

            if (fontColor.a > 0f)
                EditorGUI.LabelField(hierarchyRect, selectedObject.name, new GUIStyle
                {
                    normal = new GUIStyleState {textColor = fontColor},
                    font = fontStyleInHierarchy
                });
        }
    }
}