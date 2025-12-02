namespace _Game.Scripts.Infrastructure.States
{
    public interface IState: IExitableState
    {
        void Enter();
    }
}