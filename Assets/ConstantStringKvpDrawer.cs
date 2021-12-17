using System;
using Shared.Sources.Editor.Drawers;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StringKvp<>), true)]
[CustomPropertyDrawer(typeof(ConstantStringKvpAttribute), true)]
public class ConstantStringKvpDrawer : ConstantStringDrawer
{
    private bool _foldout;

    private bool IsProperty => GetPropertyAttribute() != null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = GetPropertyAttribute();

        if (attr == null)
        {
            attr = GetTypeCustomAttribute(fieldInfo.FieldType);
        }

        InitConstants(attr.Source, attr.Flatten);

        if (IsProperty)
        {
            var foldoutRect = new Rect(position)
            {
                height = LineHeight
            };

            _foldout = EditorGUI.Foldout(foldoutRect, _foldout, property.displayName);

            EditorGUI.indentLevel++;

            if (_foldout)
            {
                var popupRect = new Rect(position)
                {
                    height = LineHeight,
                    y = GetNextPropertyPosY(foldoutRect)
                };

                DrawKvp(position, property, popupRect);
            }
                
            EditorGUI.indentLevel--;
        }
        else
        {
            var popupRect = new Rect(position)
            {
                height = LineHeight,
            };

            DrawKvp(position, property, popupRect);
        }
    }

    private void DrawKvp(Rect position, SerializedProperty property, Rect popupRect)
    {
        var key = property.FindPropertyRelative("_key");
        DrawStringPopup(popupRect, key, _constants);

        var keyRect = new Rect(position)
        {
            height = LineHeight,
            y = GetNextPropertyPosY(popupRect)
        };

        EditorGUI.PropertyField(keyRect, key, GUIContent.none);

        var value = property.FindPropertyRelative("_value");

        var valueRect = new Rect(position)
        {
            height = EditorGUI.GetPropertyHeight(value, true),
            y = GetNextPropertyPosY(keyRect)
        };

        EditorGUI.PropertyField(valueRect, value);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return IsProperty ? GetHeightForProperty(property, label) : GetHeightForType(property, label);
    }

    private float GetHeightForType(SerializedProperty property, GUIContent label)
    {
        var value = property.FindPropertyRelative("_value");
        return EditorGUI.GetPropertyHeight(value, true) +
               (LineHeight + VerticalSpacing) * 2;
    }
        
    private float GetHeightForProperty(SerializedProperty property, GUIContent label)
    {
        if (!_foldout)
        {
            return LineHeight + VerticalSpacing;
        }
            
        var value = property.FindPropertyRelative("_value");
        return EditorGUI.GetPropertyHeight(value, true) +
               (LineHeight + VerticalSpacing) * 3;
    }
        
        
        
    private ConstantStringKvpAttribute GetTypeCustomAttribute(Type type)
    {
        var attributes = type.GetCustomAttributes(typeof(ConstantStringKvpAttribute), false);
        if (attributes.Length == 0)
        {
            var genericArgs = type.GetGenericArguments();

            foreach (var genericArg in genericArgs)
            {
                var attr = GetTypeCustomAttribute(genericArg);
                if (attr != null)
                {
                    return attr;
                }
            }
                
            return null;
        }

        return attributes[0] as ConstantStringKvpAttribute;
    }
}