using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Authoring
{
    public class StructComponentAuthoring<TComponent> : ComponentAuthoring<TComponent>
        where TComponent : struct
    {
        [SerializeField]
        private TComponent _component;

        public override TComponent Component => _component;
    }
}
