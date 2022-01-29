using UnityEngine;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Authoring
{
    public abstract class ComponentAuthoring<TComponent> : MonoBehaviour
        where TComponent : struct
    {
        [SerializeField]
        private TComponent _component;

        public TComponent Component => _component;
    }
}
