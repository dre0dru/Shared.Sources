using Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Views;

namespace Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Authoring
{
    public class MonoComponentAuthoring<TComponent, TMono> : ComponentAuthoring<TComponent>
        where TComponent : struct, IMonoComponent<TMono>
    {
        public override TComponent Component
        {
            get
            {
                var component = GetComponentInChildren<TMono>();

                return new TComponent()
                {
                    Component = component
                };
            }
        }
    }
}
