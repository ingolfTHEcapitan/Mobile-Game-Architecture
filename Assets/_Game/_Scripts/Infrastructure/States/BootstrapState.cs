using _Game._Scripts.Infrastructure.AssetManagement;
using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.Input;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.States
{
    public class BootstrapState: IState
    {
        private const string InitialScene = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            // Сервисы регистрируем в конструкторе, что бы при создании состояния
            // в стейт машине сразу проинициализировать все необходимые сервисы.
            RegisterServices();
        }

        public void Enter()
        {
            // Нужно всегда начинать с начала с Initial, не зависимо от того откуда мы пришли в BootstrapState
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistantProgressService>(), 
                _services.Single<IGameFactory>()));
        }

        private static IInputService GetInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}