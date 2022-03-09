using System.Runtime.CompilerServices;
using Leopotam.EcsLite;
using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Extensions
{
    public static class EcsFilterExtensions
    {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int First(this EcsFilter filter)
        {
            Debug.Assert(filter.GetEntitiesCount() > 0);

            return filter.GetRawEntities()[0];
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFirst(this EcsFilter filter, out int entity)
        {
            entity = -1;
            
            if (filter.GetEntitiesCount() == 0)
            {
                return false;
            }
            
            entity = filter.First();
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static int Single(this EcsFilter filter)
        {
            Debug.Assert(filter.GetEntitiesCount() == 1);

            return filter.First();
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSingle(this EcsFilter filter, out int entity)
        {
            entity = -1;
            
            if (filter.GetEntitiesCount() == 0)
            {
                return false;
            }

            Debug.Assert(filter.GetEntitiesCount() == 1);

            entity = filter.First();
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Any(this EcsFilter filter)
        {
            return filter.GetEntitiesCount() > 0;
        }
    }
}
