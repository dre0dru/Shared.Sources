using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor
{
    public static class AssetDatabaseUtils
    {
        public static string GetTypeName<T>()
        {
            return typeof(T).Name;
        }

        public static T FindFirstAsset<T>()
            where T : Object
        {
            var guids = FindAssetGuidsByType<T>();

            return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids.First()));
        }

        public static T FindSingleAsset<T>()
            where T : Object
        {
            var guids = FindAssetGuidsByType<T>();

            return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids.Single()));
        }

        public static string[] FindAssetGuidsByType<T>()
            where T : Object
        {
            return AssetDatabase.FindAssets($"t: {GetTypeName<T>()}");
        }
    }
}
