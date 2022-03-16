using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shared.Sources.CustomAttributes;
using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ConstantStringAttribute))]
    public class ConstantStringPropertyDrawer : PropertyDrawer
    {
        protected string[] _constants;
        protected float LineHeight => EditorGUIUtility.singleLineHeight;
        protected float VerticalSpacing => EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = GetPropertyAttribute();

            InitConstants(attr.Source, attr.Flatten);            

            var popupRect = new Rect(position)
            {
                height = LineHeight,
            };

            DrawStringPopup(popupRect, property, _constants);

            var keyRect = new Rect(position)
            {
                height = LineHeight,
                y = GetNextPropertyPosY(popupRect)
            };

            EditorGUI.PropertyField(keyRect, property, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return LineHeight * 2 + VerticalSpacing;;
        }

        protected void InitConstants(Type source, bool flatten)
        {
            _constants ??= GetConstantsTree(source, flatten);
        }

        protected ConstantStringAttribute GetPropertyAttribute()
        {
            return attribute as ConstantStringAttribute;
        }

        protected string[] GetConstantsTree(Type type, bool flatten)
        {
            var result = new List<string>();

            result.AddRange(GetTypeConstants(type));

            var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);

            if (nestedTypes.Length > 0)
            {
                foreach (var nestedType in nestedTypes)
                {
                    var constants = GetConstantsTree(nestedType, flatten);
                    
                    if (flatten)
                    {
                        result.AddRange(constants);
                    }
                    else
                    {
                        result.AddRange(constants.Select(s => $"{nestedType.Name}/{s}"));
                    }
                }
            }
            
            return result.ToArray();
        }

        protected IEnumerable<string> GetTypeConstants(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(info => info.IsLiteral && !info.IsInitOnly)
                .Select(info => (string)info.GetRawConstantValue());
        }
        
        protected float GetNextPropertyPosY(Rect previousRect) =>
            previousRect.y + previousRect.height + VerticalSpacing;

        protected void DrawStringPopup(Rect popupRect, SerializedProperty property, string[] options)
        {
            var currentValue = property.stringValue;
            var currentIndex = Array.IndexOf<string>(options, currentValue);

            if (currentIndex == -1)
            {
                var popupRectLeft = new Rect(popupRect);
                popupRectLeft.width /= 2;

                DrawPopup(popupRectLeft, property, currentIndex, options);

                var popupRectRight = new Rect(popupRectLeft);
                popupRectRight.x += popupRectRight.width;
                EditorGUI.HelpBox(popupRectRight, "No matching value selected",
                    MessageType.Error);
            }
            else
            {
                DrawPopup(popupRect, property, currentIndex, options);
            }
        }
        
        protected void DrawPopup(Rect position, SerializedProperty property, int currentIndex, string[] options)
        {
            var popupIndex = EditorGUI.Popup(position, property.displayName, currentIndex, options);

            if (popupIndex != -1)
            {
                var selectedValue = options[popupIndex];
                property.stringValue = selectedValue;
            }
        }
    }
}
