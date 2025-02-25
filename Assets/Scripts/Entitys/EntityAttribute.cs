using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EntityAttribute))]
public class XAttributesEditor : Editor
{
    private SerializedProperty attributeList;

    private void OnEnable()
    {
        attributeList = serializedObject.FindProperty("attributeList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Loop through the list of AttributeData
        for (int i = 0; i < attributeList.arraySize; i++)
        {
            SerializedProperty element = attributeList.GetArrayElementAtIndex(i);

            SerializedProperty attribute = element.FindPropertyRelative("attribute");
            SerializedProperty value = element.FindPropertyRelative("value");
            using (var _ = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(attribute, new GUIContent(""));
                EditorGUILayout.PropertyField( value, new GUIContent(""));
                if(GUILayout.Button("X", GUILayout.Width(20)))
                {
                    attributeList.DeleteArrayElementAtIndex(i);
                    break;
                }
            }
        }

        // Optionally add a button to add more attributes
        if (GUILayout.Button("Add Attribute"))
        {
            attributeList.arraySize++;
        }

        serializedObject.ApplyModifiedProperties();
    }
}


public class EntityAttribute : MonoBehaviour
{
    public EntityController host;

    // Serialize the dictionary using a custom editor
    [SerializeField]
    private List<AttributeData> attributeList = new List<AttributeData>();

    // Create a helper struct to hold the attribute type and its value for serialization
    [System.Serializable]
    public struct AttributeData
    {
        public AttributeType attribute;
        public float value;
    }

    public float GetAttribute(AttributeType type)
    {
        foreach (var attribute in attributeList)
        {
            if (attribute.attribute == type)
            {
                return attribute.value;
            }
        }
        return 0;
    }

    public void SetAttribute(AttributeType type, float value)
    {
        for (int i = 0; i < attributeList.Count; i++)
        {
            var attribute = attributeList[i];
            if (attribute.attribute == type)
            {
                attribute.value = value;
                GlobalEventManager.Instance.Fire(EventDefine.OnChangeAttribute, new AttributeEventArgs(host, type, value));
            }
        }
    }

}

internal class AttributeEventArgs : BaseEventArgs
{
    public EntityController entity;
    public AttributeType type = AttributeType.None;
    public float value;

    public AttributeEventArgs(EntityController entity, AttributeType type, float value)
    {
        this.type = type;
        this.value = value;
    }
}

