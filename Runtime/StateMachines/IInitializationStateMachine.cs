namespace Shared.Sources.StateMachines
{
    public interface IInitializationStateMachine
    {
        void StartInitialization();

        void MoveNext();

        void CompleteInitialization();
    }
    
    public interface IInitializationStateMachine<out TContext>
    {
        TContext Context { get; }
        
        void StartInitialization();

        void MoveNext();

        void CompleteInitialization();
    }
}
