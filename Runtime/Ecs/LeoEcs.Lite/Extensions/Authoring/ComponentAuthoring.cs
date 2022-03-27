using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Authoring
{
    public abstract class ComponentAuthoring<TComponent> : MonoBehaviour
    {
        public abstract TComponent Component { get; }
    }
}
