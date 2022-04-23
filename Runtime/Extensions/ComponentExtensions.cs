using UnityEngine;

namespace Shared.Sources.Extensions
{
    public static class ComponentExtensions
    {
        public static T AddComponent<T>(this Component component)
            where T : Component
        {
            return component.gameObject.AddComponent<T>();
        }
        
        public static T GetOrAddComponent<T>(this Component component)
            where T : Component
        {
            if (!component.TryGetComponent<T>(out var result))
            {
                result = component.AddComponent<T>();
            }

            return result;
        }
    }
}
