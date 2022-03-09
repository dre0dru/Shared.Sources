using Leopotam.EcsLite;
using Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Components;
using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Extensions
{
    public static class GameObjectExtensions
    {
        public static void MarkAsEntityOwner(this GameObject gameObject, EcsPackedEntity owner)
        {
            if (gameObject.TryGetComponent<EcsEntityOwner>(out var ecsEntityOwner))
            {
                ecsEntityOwner.Owner = owner;
            }
            else
            {
                gameObject.AddComponent<EcsEntityOwner>().Owner = owner;
            }
        }
    }
}
