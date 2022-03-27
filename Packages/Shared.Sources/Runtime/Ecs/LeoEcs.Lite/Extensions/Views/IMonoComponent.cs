namespace Shared.Sources.Ecs.LeoEcs.Lite.Extensions.Views
{
    public interface IMonoComponent<TMono>
    {
        public TMono Component { get; set; }
    }
}
