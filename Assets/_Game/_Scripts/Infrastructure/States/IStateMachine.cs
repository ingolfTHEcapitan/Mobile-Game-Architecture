using _Game._Scripts.Infrastructure.Services;

namespace _Game._Scripts.Infrastructure.States
{
    public interface IStateMachine: IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>;
    }
}