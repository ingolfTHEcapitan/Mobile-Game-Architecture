using _Game._Scripts.Logic;

namespace _Game._Scripts.Enemy
{
    public interface IAnimationStateReader
    {
        AnimatorState State { get;}

        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}