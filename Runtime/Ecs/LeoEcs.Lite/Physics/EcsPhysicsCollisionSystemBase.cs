using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using MessagePipe;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Physics
{
    public abstract class EcsPhysicsCollisionSystemBase : IEcsInitSystem, IEcsDestroySystem
    {
        protected readonly Queue<CollisionEvent> _collisionEvents;

        private IDisposable _collisionSubscriber;
        
        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        protected EcsPhysicsCollisionSystemBase()
        {
            _collisionEvents = new Queue<CollisionEvent>();
        }

        public virtual void Init(EcsSystems systems)
        {
            _collisionSubscriber = GlobalMessagePipe.GetSubscriber<CollisionEvent>().Subscribe(OnCollisionEvent);
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
