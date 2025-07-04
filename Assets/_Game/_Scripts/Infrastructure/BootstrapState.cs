using UnityEngine;
using _Game._Scripts.Services.Input;

namespace _Game._Scripts.Infrastructure
{
    public class BootstrapState: IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            RegisterService();
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