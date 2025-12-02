using System;
using System.Collections.Generic;
using _Game.Scripts.Infrastructure.States.GameStates;
using _Game.Scripts.Services;
using _Game.Scripts.Services.Factory;
using _Game.Scripts.Services.PersistantProgress;
using _Game.Scripts.Services.SaveLoad;
using _Game.Scripts.Services.StaticData;
using _Game.Scripts.UI.Elements;
using _Game.Scripts.UI.Services.Factory;

namespace _Game.Scripts.Infrastructure.States
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                    services.Single<IPersistantProgressService>(), 
                    services.Single<ISaveLoadService>()
                ),
                
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, 
                    services.Single<IGameFactory>(), 
                    services.Single<IPersistantProgressService>(), 
                    services.Single<IStaticDataService>(),
                    services.Single<IUIFactory>(), 
                    services.Single<ISaveLoadService>()), 
                
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state?.Enter();
        }
        
        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
        {
            IPayLoadedState<TPayLoad> state = ChangeState<TState>();
            state?.Enter(payLoad); 
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            // Проверка на null нужна потому что
            // при первом заходе в состояние у нас не будет активного состояния что бы с него выйти.
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}