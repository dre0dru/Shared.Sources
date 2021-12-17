using System;
using System.Linq;
using System.Reflection;
using Shared.Sources.CustomDrawers;
using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ConstantStringAttribute))]
    public class ConstantStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as ConstantStringAttribute;
            var source = attr.Source;
            
            var constants = GetConstants(attr.Source);

            var popupRect = new Rect(position);
            popupRect.height /= 2;

            var currentValue = property.stringValue;
            var currentIndex = Array.IndexOf<string>(constants, currentValue);

            if (currentIndex == -1)
            {
                var popupRectLeft = new Rect(popupRect);
                popupRectLeft.width /= 2;
                
                DrawPopup(popupRectLeft, property, currentIndex, constants);

                var popupRectRight = new Rect(popupRectLeft);
                popupRectRight.x += popupRectRight.width;
                EditorGUI.HelpBox(popupRectRight, $"No matching value selected from [{source.Name}]", MessageType.Error);
            }
            else
            {
                DrawPopup(popupRect, property, currentIndex, constants);
            }
            
            DrawPropertyField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + EditorGUIUtility.standardVerticalSpacing;
        }

        private void DrawPopup(Rect position, SerializedProperty property, int currentIndex, string[] options)
        {
            var popupIndex = EditorGUI.Popup(position, property.displayName, currentIndex, options);

            if (popupIndex != -1)
            {
                var selectedValue = options[popupIndex];
                property.stringValue = selectedValue;
            }
        }

        private void DrawPropertyField(Rect position, SerializedProperty property, GUIContent label)
        {
            var propertyRect = new Rect(position);
            propertyRect.height /= 2;
            propertyRect.y += propertyRect.height + 1;

            EditorGUI.PropertyField(propertyRect, property, label);
        }

        private string[] GetConstants(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(info => info.IsLiteral && !info.IsInitOnly)
                .Select(info => (string)info.GetRawConstantValue())
                .ToArray();
        }
    }
}
