using _Game._Scripts.Enemy;
using _Game._Scripts.Hero;
using _Game._Scripts.Infrastructure.Services.Factory;
using _Game._Scripts.Infrastructure.Services.Input;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.Logic;
using _Game._Scripts.Logic.Camera;
using _Game._Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game._Scripts.Infrastructure.States.GameStates
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
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistantProgressService progressService, IInputService inputService, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _inputService = inputService;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanupProgressReadersWriters();
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
            InitEnemySpawners();
            InitLootPieces();

            GameObject hero = InitHero();

            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitEnemySpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateEnemySpawner(spawnerData.SpawnerId, spawnerData.EnemyTypeId, spawnerData.Position);
            }
        }

        private void InitLootPieces()
        {
            foreach (string key in _progressService.Progress.WorldData.LootData.LootPiecesOnScene.Dictionary.Keys)
            {
                LootPiece lootPiece = _gameFactory.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = key;
            }
        }
        
        private GameObject InitHero()
        {
            GameObject hero =  _gameFactory.CreateHero(
                at: GameObject.FindWithTag(InitialPointTag),
                parent: GameObject.FindWithTag(GameTag));
            
            hero.GetComponent<HerroAttack>().Initialize(_inputService);
            return hero;
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud(parent: GameObject.FindWithTag(UITag));
            
            ActorUI actorUI = hud.GetComponentInChildren<ActorUI>();
            actorUI.Initialize(hero.GetComponent<HeroHealth>());
            
            LootCounter lootCounter = hud.GetComponentInChildren<LootCounter>();
            lootCounter.Initialize(_progressService.Progress.WorldData);
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