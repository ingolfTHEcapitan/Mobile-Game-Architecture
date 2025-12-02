using _Game.Scripts.Services;

namespace _Game.Scripts.Infrastructure.States
{
    public interface IStateMachine: IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>;
    }
}