using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    /// <summary>
    /// Class that responsible for custom view in hierarchy view
    /// </summary>
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class HierarchyView
    {
        private const float BACKGROUND_DRAWING_OFFSET = 16f;
        private const float BACKGROUND_MAXIMUM_X_POS_SCALE = .9f;

        static HierarchyView()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= DrawCustomHierarchyGui;
            EditorApplication.hierarchyWindowItemOnGUI += DrawCustomHierarchyGui;
        }

        private static void DrawCustomHierarchyGui(int instanceId, Rect hierarchyRect)
        {
            GameObject selectedObject = null;
            HierarchyHighlighter hierarchyHighlighter = null;
            FontStyle fontStyleInHierarchy;
            Color backgroundColor;
            Color fontColor;
            int fontSizeInHierarchy;

            selectedObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (selectedObject != null)
                hierarchyHighlighter = selectedObject.GetComponent<HierarchyHighlighter>();

            if (hierarchyHighlighter == null || !hierarchyHighlighter.IsDisplayed)
                return;

            fontStyleInHierarchy = hierarchyHighlighter.FontStyle;
            backgroundColor = hierarchyHighlighter.BackgroundColor;
            fontColor = hierarchyHighlighter.FontColor;
            fontSizeInHierarchy = hierarchyHighlighter.FontSize;

            hierarchyRect.center += Vector2.right * BACKGROUND_DRAWING_OFFSET;
            hierarchyRect.xMax *= BACKGROUND_MAXIMUM_X_POS_SCALE;

            if (backgroundColor.a > 0f)
                EditorGUI.DrawRect(hierarchyRect, backgroundColor);

            if (fontColor.a > 0f)
                EditorGUI.LabelField(hierarchyRect, selectedObject.name, new GUIStyle
                {
                    fontSize = fontSizeInHierarchy,
                    fontStyle = fontStyleInHierarchy
                });
        }
    }
#endif
}