using MessagePipe;
using Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Components;
using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Physics
{
    public class EcsPhysicsOnCollisionEmitter : MonoBehaviour
    {
        private IPublisher<CollisionEvent> _collisionPublisher;

        private void Awake()
        {
            _collisionPublisher = GlobalMessagePipe.GetPublisher<CollisionEvent>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (TryGetComponent<EcsEntityOwner>(out var owner) &&
                collision.gameObject.TryGetComponent<EcsEntityOwner>(out var collidedWith))
            {
                _collisionPublisher.Publish(new CollisionEvent()
                {
                    Owner = owner.Owner,
                    CollidedWith = collidedWith.Owner,
                    Source = gameObject,
                    Collision = collision
                });
            }
        }
    }
}
