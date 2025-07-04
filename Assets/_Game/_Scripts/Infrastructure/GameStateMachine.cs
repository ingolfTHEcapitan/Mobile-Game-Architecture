
using System;
using System.Collections.Generic;

namespace _Game._Scripts.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                { typeof(BootstrapState),  new BootstrapState(this) }
            };
        }
        
        public void Enter<TState>() where TState : IState
        {
            // Проверка на null нужна потому что
            // при первом заходе в состояние у нас не будет активного состояния что бы с него выйти.
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            _activeState.Enter();
        }
    }
}