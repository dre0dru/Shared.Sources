using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using MessagePipe;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Physics
{
    public abstract class EcsPhysicsCollisionSystemBase : IEcsDestroySystem
    {
        protected readonly Queue<CollisionEvent> _collisionEvents;
        private readonly IDisposable _collisionSubscriber;
        
        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        protected EcsPhysicsCollisionSystemBase(ISubscriber<CollisionEvent> collisionEventsSubscriber)
        {
            _collisionEvents = new Queue<CollisionEvent>();
            _collisionSubscriber = collisionEventsSubscriber.Subscribe(OnCollisionEvent);
        }

        public virtual void Destroy(EcsSystems systems)
        {
            _collisionSubscriber.Dispose();
        }

        private void OnCollisionEvent(CollisionEvent collisionEvent)
        {
            _collisionEvents.Enqueue(collisionEvent);
        }
    }
}
