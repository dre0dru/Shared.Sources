using Leopotam.EcsLite;
using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Components
{
    [DisallowMultipleComponent]
    public class EcsEntityOwner : MonoBehaviour
    {
        public EcsPackedEntity Owner { get; set; }
    }
}
