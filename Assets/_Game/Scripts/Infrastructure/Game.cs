using _Game.Scripts.Infrastructure.States;
using _Game.Scripts.Services;
using _Game.Scripts.UI.Elements;

namespace _Game.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}