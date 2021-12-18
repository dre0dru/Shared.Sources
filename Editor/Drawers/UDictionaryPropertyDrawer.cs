using Shared.Sources.Collections;
using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(UDictionary<,,>), true)]
    public class UDictionaryPropertyDrawer : PropertyDrawer
    {
        private float LineHeight => EditorGUIUtility.singleLineHeight;
        private float VerticalSpacing => EditorGUIUtility.standardVerticalSpacing;
        private float HelpBoxHeight => LineHeight * 1.5f;

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            var serializedList = GetSerializedList(property);

            if (serializedList == null)
            {
                var helpBoxPos = new Rect(pos)
                {
                    height = HelpBoxHeight
                };

                EditorGUI.HelpBox(helpBoxPos, "Nothing to draw, check ensure that IKvp is serializable", MessageType.Error);
                return;
            }

            EditorGUI.PropertyField(pos, serializedList, label, true);

            if (HasCollisions(property))
            {
                var helpBoxPos = new Rect(pos)
                {
                    height = HelpBoxHeight
                };

                helpBoxPos.y += EditorGUI.GetPropertyHeight(serializedList, true) + VerticalSpacing;
                
                EditorGUI.HelpBox(helpBoxPos, $"{property.displayName} contains duplicate keys", MessageType.Error);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var serializedList = GetSerializedList(property);

            if (serializedList == null)
            {
                return LineHeight * 2;
            }
            
            var height = 0f;
            height += EditorGUI.GetPropertyHeight(serializedList, true);

            if (HasCollisions(property))
            {
                height += HelpBoxHeight + VerticalSpacing;
            }

            return height;
        }

        private SerializedProperty GetSerializedList(SerializedProperty property)
        {
            return property.FindPropertyRelative("_serialized");
        }

        private bool HasCollisions(SerializedProperty property)
        {
            return property.FindPropertyRelative("_hasCollisions").boolValue;
        }
    }
}