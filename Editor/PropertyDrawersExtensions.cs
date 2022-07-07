using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace Shared.Sources.Editor
{
    public static class PropertyDrawersExtensions
    {
        public static object GetObjectValue(this SerializedProperty serializedProperty)
        {
            return GetTargetObjectOfProperty(serializedProperty);
        }

        public static object GetObjectValue<T>(this SerializedProperty serializedProperty)
        {
            return (T)serializedProperty.GetObjectValue();
        }

        private static object GetTargetObjectOfProperty(SerializedProperty serializedProperty)
        {
            var path = serializedProperty.propertyPath.Replace(".Array.data[", "[");
            object targetObject = serializedProperty.serializedObject.targetObject;
            var elements = path.Split('.');
            
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("[", StringComparison.Ordinal));
                    var index = Convert.ToInt32(element
                        .Substring(element.IndexOf("[", StringComparison.Ordinal))
                        .Replace("[", "")
                        .Replace("]", ""));
                    targetObject = GetValue(targetObject, elementName, index);
                }
                else
                {
                    targetObject = GetValue(targetObject, element);
                }
            }

            return targetObject;
        }

        private static object GetValue(object source, string name, int index)
        {
            if (GetValue(source, name) is not IEnumerable enumerable)
            {
                return null;
            }

            var enumerator = enumerable.GetEnumerator();

            for (int i = 0; i <= index; i++)
            {
                if (!enumerator.MoveNext()) return null;
            }

            return enumerator.Current;
        }

        private static object GetValue(object source, string name)
        {
            if (source == null)
            {
                return null;
            }
            
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, 
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                {
                    return f.GetValue(source);
                }

                var p = type.GetProperty(name,
                    BindingFlags.NonPublic | BindingFlags.Public | 
                    BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                {
                    return p.GetValue(source, null);
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}
