using UnityEngine;
using _Game._Scripts.Services.Input;

namespace _Game._Scripts.Infrastructure
{
    public class BootstrapState: IState
    {
        private const string InitialScene = "Initial";
        private const string GameScene = "Game";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterService();
            // Нужно всегда начинать с начала с Initial, не зависимо от того откуда мы пришли в BootstrapState
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

         private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameScene);
        }

        public void Exit()
        {
            
        }
        
        private void RegisterService()
        {
            Game.InputService = RegisterInputService();
        }

        private static IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

    }
}