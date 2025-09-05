using _Game._Scripts.Enemy;
using _Game._Scripts.Hero;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.Factory;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.Logic;
using _Game._Scripts.StaticData;
using _Game._Scripts.UI.Elements;
using _Game._Scripts.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game._Scripts.Infrastructure.States.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistantProgressService progressService,
            IStaticDataService staticData, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
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
            InitPopUpLayer();
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitPopUpLayer()
        {
            _uiFactory.CreatePopUpLayer();
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
                at: GameObject.FindWithTag(SceneTag.InitialPoint),
                parent: GameObject.FindWithTag(SceneTag.Game));
            
            return hero;
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud(parent: GameObject.FindWithTag(SceneTag.UI));
            
            HealthBarView healthBarView = hud.GetComponentInChildren<HealthBarView>();
            healthBarView.Initialize(hero.GetComponent<HeroHealth>());
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