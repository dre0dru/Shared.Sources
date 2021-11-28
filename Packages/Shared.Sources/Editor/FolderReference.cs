using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor
{
    [System.Serializable]
    public class FolderReference
    {
        public string GUID;

        public string Path => AssetDatabase.GUIDToAssetPath(GUID);
    }

    [CustomPropertyDrawer(typeof(FolderReference))]
    public class FolderReferencePropertyDrawer : PropertyDrawer
    {
        private const string GuidProperty = "GUID";
        private const string Label = "Folder";

        private DefaultAsset _targetFolder;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var guidProperty = property.FindPropertyRelative(GuidProperty);

            if (string.IsNullOrEmpty(guidProperty.stringValue) == false)
            {
                var folderPath = AssetDatabase.GUIDToAssetPath(guidProperty.stringValue);
                _targetFolder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(folderPath);
            }

            var propertyHeight = EditorGUI.GetPropertyHeight(guidProperty);
            var propertyRect = new Rect(position.x, position.y, position.width, propertyHeight);
            position.y += propertyHeight;

            _targetFolder =
                (DefaultAsset)EditorGUI.ObjectField(propertyRect, Label, _targetFolder, typeof(DefaultAsset), false);

            if (_targetFolder != null)
            {
                var selectionPath = AssetDatabase.GetAssetPath(_targetFolder);

                if (string.IsNullOrEmpty(selectionPath) == false)
                {
                    var selectedGuid = AssetDatabase.AssetPathToGUID(selectionPath);
                    guidProperty.stringValue = selectedGuid;
                }
            }

            EditorGUI.EndProperty();
        }
    }
}
