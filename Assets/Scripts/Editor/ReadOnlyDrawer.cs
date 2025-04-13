using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Disable the property field to make it read-only
        GUI.enabled = false;

        // Draw the property field
        EditorGUI.PropertyField(position, property, label, true);

        // Re-enable the property field
        GUI.enabled = true;
    }
}
  

