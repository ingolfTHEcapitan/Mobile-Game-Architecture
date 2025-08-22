namespace _Game._Scripts.Logic.Animation
{
    public interface IAnimationStateReader
    {
        AnimatorState State { get;}

        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}