using Leopotam.EcsLite;
using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Physics
{
    public struct CollisionEvent
    {
        public EcsPackedEntity Owner;
        public EcsPackedEntity CollidedWith;
        public GameObject Source;
        public Collision Collision;
    }
}
