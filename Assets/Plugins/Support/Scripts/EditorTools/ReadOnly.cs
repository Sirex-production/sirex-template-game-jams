using System;
using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    /// <summary>
    /// Attribute that defines custom view for ReadOnlyAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.DrawRect(position, new Color(0f, 0f, 0f, .1f));
            GUI.enabled = true;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}