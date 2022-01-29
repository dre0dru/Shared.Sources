using System.Runtime.CompilerServices;
using Leopotam.EcsLite;
using Shared.Sources.Ecs.LeoEcs.Lite.Authoring;
using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite
{
    public static class EcsPoolExtensions
    {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ref T AddFromAuthoring<T>(this EcsPool<T> pool, int entity, GameObject authoringSource)
            where T : struct
        {
            ref var component = ref pool.Add(entity);
            component = authoringSource.GetComponent<ComponentAuthoring<T>>().Component;

            return ref component;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(this EcsPool<T> pool, int entity, T component)
            where T : struct
        {
            ref var newComponent = ref pool.Add(entity);

            newComponent = component;

            return ref newComponent;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool TryGet<T>(this EcsPool<T> pool, int entity, out T component)
            where T : struct
        {
            component = default;
            
            if (pool.Has(entity))
            {
                component = ref pool.Get(entity);
                return true;
            }

            return false;
        }
    }
}