using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor
{
    public static class AssetDatabaseUtils
    {
        public static T FindAsset<T>()
            where T : Object
        {
            var filter = typeof(T).Name;
            string[] assets = AssetDatabase.FindAssets($"t: {filter}");
            if (assets != null && assets.Length > 0)
            {
                T asset =
                    AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assets[0]));

                return asset;
            }

            return null;
        }
    }
}
