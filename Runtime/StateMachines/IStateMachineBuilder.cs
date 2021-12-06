namespace Shared.Sources.StateMachines
{
    public interface IStateMachineBuilder<in TState, out TStateMachine>
    {
        IStateMachineBuilder<TState, TStateMachine> Add(TState state);

        TStateMachine Build();
    }
    
    public interface IStateMachineBuilder<in TState, out TStateMachine, in TSettings>
    {
        IStateMachineBuilder<TState, TStateMachine> Add(TState state);

        TStateMachine Build(TSettings settings);
    }
}
