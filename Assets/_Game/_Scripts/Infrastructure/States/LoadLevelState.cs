using _Game._Scripts.CameraLogic;
using _Game._Scripts.Hero;
using _Game._Scripts.Infrastructure.Factory;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Logic;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string GameTag = "Game";
        private const string UITag = "UI";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistantProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            // При загрузке новой сцены очищаем список тех кому нужен прогресс
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject hero = InitHero();

            InitHud(hero);
            CameraFollow(hero);
        }

        private GameObject InitHero()
        {
            return _gameFactory.CreateHero(
                at: GameObject.FindWithTag(InitialPointTag),
                parent: GameObject.FindWithTag(GameTag));
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud(parent: GameObject.FindWithTag(UITag));
            
            ActorUI actorUI = hud.GetComponentInChildren<ActorUI>();
            actorUI.Initialize(hero.GetComponent<HeroHealth>());
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void CameraFollow(GameObject target)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(target);
        }
    }
}