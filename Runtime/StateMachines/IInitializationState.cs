namespace Shared.Sources.StateMachines
{
    public interface IInitializationState<in TPayload>
    {
        void Enter(TPayload payload);
    }
}
