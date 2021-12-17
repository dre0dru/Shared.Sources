#if UNITY_2020_1_OR_NEWER
using Shared.Sources.Collections;
using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor.Drawers
{
    /// <summary>
    /// Draws the generic dictionary a bit nicer than Unity would natively (not as many expand-arrows
    /// and better spacing between KeyValue pairs). Also renders a warning-box if there are duplicate
    /// keys in the dictionary.
    /// </summary>
    [CustomPropertyDrawer(typeof(UDictionary<,>))]
    public class UDictionaryPropertyDrawer : PropertyDrawer
    {
        private float LineHeight => EditorGUIUtility.singleLineHeight;
        private float VerticalSpacing => EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            var serializedList = GetSerializedList(property);

            var currentPos = new Rect(LineHeight, pos.y, pos.width, LineHeight);
            EditorGUI.PropertyField(currentPos, serializedList, label, true);

            if (HasCollisions(property))
            {
                currentPos.y += EditorGUI.GetPropertyHeight(serializedList, true) + VerticalSpacing;
                var entryPos = new Rect(LineHeight, currentPos.y, pos.width, LineHeight);
                EditorGUI.HelpBox(entryPos, "Dictionary contains duplicate keys", MessageType.Error);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totHeight = 0f;

            var listProp = GetSerializedList(property);
            totHeight += EditorGUI.GetPropertyHeight(listProp, true);

            if (HasCollisions(property))
            {
                totHeight += LineHeight  + VerticalSpacing;
            }

            return totHeight;
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
#endif
