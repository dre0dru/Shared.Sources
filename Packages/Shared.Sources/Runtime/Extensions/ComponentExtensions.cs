using System;
using UnityEngine;
using UnityEngine.Pool;

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

        public static void ExecuteDownwards<TComponent>(this Component root, Action<TComponent> action,
            bool includeInactive = false)
            where TComponent : Component =>
            root.gameObject.ExecuteDownwards<TComponent>(action, includeInactive);

        public static void ExecuteUpwards<TComponent>(this Component root, Action<TComponent> action,
            bool includeInactive = false)
            where TComponent : Component =>
            root.gameObject.ExecuteUpwards<TComponent>(action, includeInactive);

        public static TBehaviour Enable<TBehaviour>(this TBehaviour behaviour)
            where TBehaviour : Behaviour
        {
            behaviour.enabled = true;
            return behaviour;
        }

        public static TBehaviour Disable<TBehaviour>(this TBehaviour behaviour)
            where TBehaviour : Behaviour
        {
            behaviour.enabled = false;
            return behaviour;
        }
    }
}
