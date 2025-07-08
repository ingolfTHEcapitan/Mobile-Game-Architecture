using _Game._Scripts.Infrastructure.AssetManagement;
using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Services.Input;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.States
{
    public class BootstrapState: IState
    {
        private const string InitialScene = "Initial";
        private const string GameScene = "Game";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            // ������� ������������ � ������������, ��� �� ��� �������� ���������
            // � ����� ������ ����� ������������������� ��� ����������� �������.
            RegisterServices();
        }

        public void Enter()
        {
            // ����� ������ �������� � ������ � Initial, �� �������� �� ���� ������ �� ������ � BootstrapState
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameScene);
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