using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.Ads;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.Factory;
using _Game._Scripts.Infrastructure.Services.Input;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.SaveLoad;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.UI.Services.Factory;
using _Game._Scripts.UI.Services.Windows;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.States.GameStates
{
    public class BootstrapState : IState
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
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadProgress);
        }

        public void Exit()
        {

        }

        private void EnterLoadProgress()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAdsService>(InitializeAdsService());
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IStaticDataService>(InitializeStaticDataService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());

            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssetProvider>(), 
                _services.Single<IStaticDataService>(),
                _services.Single<IPersistantProgressService>(), 
                _services.Single<IAdsService>()
            ));
            
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IPersistantProgressService>(),
                _services.Single<IInputService>(),
                _services.Single<IWindowService>()
            ));

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistantProgressService>(),
                _services.Single<IGameFactory>()
            ));
        }

        private static IInputService GetInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        private StaticDataService InitializeStaticDataService()
        {
            var staticData = new StaticDataService();
            staticData.LoadEnemies();
            staticData.LoadLevels();
            staticData.LoadWindows();
            return staticData;
        }

        private AdsService InitializeAdsService()
        {
            var adsService = new AdsService();
            adsService.Initialize();
            return adsService;
        }
    }
}