using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class DynamicTitleEditor : Editor
{
    public override void OnInspectorGUI()
    {

        MonoBehaviour targetComponent = (MonoBehaviour)target;

        var dynamicTitleAttribute = targetComponent.GetType()
            .GetCustomAttributes(typeof(DynamicTitleAttribute), true)
            .FirstOrDefault() as DynamicTitleAttribute;

        if (dynamicTitleAttribute != null)
        {
            object[] args = new Object[dynamicTitleAttribute.argNames.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = getPropertyValue(dynamicTitleAttribute.argNames[i], targetComponent);
            }

            string title = string.Format(dynamicTitleAttribute.title, args);

            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
            EditorGUILayout.Space();

        } else
        {
            EditorGUILayout.LabelField("empty", EditorStyles.boldLabel);
        }
        DrawDefaultInspector();
    }

    private object getPropertyValue(string name, MonoBehaviour targetComponent)
    {
        FieldInfo fieldInfo = targetComponent.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        PropertyInfo propertyInfo = targetComponent.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (fieldInfo != null)
        {
            return fieldInfo.GetValue(targetComponent);
        }
        else if (propertyInfo != null)
        {
            return propertyInfo.GetValue(targetComponent, null);
        }
        else
        {
            return $"[Could not find: {name}]";
        }
    }
}
