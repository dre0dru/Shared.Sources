using UnityEditor;

namespace Shared.Sources.Editor
{
    public static class PropertyDrawersExtensions
    {
        public static object GetObjectValue(this SerializedProperty serializedProperty, PropertyDrawer propertyDrawer)
        {
            return propertyDrawer.fieldInfo.GetValue(serializedProperty.serializedObject.targetObject);
        }
        
        public static object GetObjectValue<T>(this SerializedProperty serializedProperty, PropertyDrawer propertyDrawer)
        {
            return (T)propertyDrawer.fieldInfo.GetValue(serializedProperty.serializedObject.targetObject);
        }
    }
}
