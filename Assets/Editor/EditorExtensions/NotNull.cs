using System;
using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    /// <summary>
    /// Attribute that defines custom view for NotNullAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(NotNullAttribute))]
    public class NotNullDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.objectReferenceValue == null)
            {
                GUI.color = Color.red;
                label.text = $"[NULL] {property.name}";
            }

            EditorGUI.PropertyField(position, property, label);

            GUI.color = Color.white;
            EditorGUI.EndProperty();
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class NotNullAttribute : PropertyAttribute { }
}