namespace _Game.Scripts.Infrastructure.States
{
    public interface IPayLoadedState<TPayLoad>: IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
}