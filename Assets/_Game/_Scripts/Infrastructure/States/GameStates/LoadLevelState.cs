using System.Collections.Generic;
using _Game._Scripts.Enemy;
using _Game._Scripts.Hero;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.Factory;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.SaveLoad;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.Logic;
using _Game._Scripts.Logic.EnemySpawner;
using _Game._Scripts.Logic.Triggers;
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
        private readonly ISaveLoadService _saveLoadService;
        
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistantProgressService progressService,
            IStaticDataService staticData, IUIFactory uiFactory, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
            _saveLoadService = saveLoadService;
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
            LevelStaticData levelData = GetLevelData();
            InitSaveTriggers();
            InitLevelTransferTriggers();
            InitEnemySpawners(levelData);
            InitLootPieces();

            GameObject hero = InitHero(levelData);

            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitSaveTriggers()
        {
            foreach (var saveTriggerObject in GameObject.FindGameObjectsWithTag(Tags.SaveTrigger)) 
                saveTriggerObject.GetComponent<SaveTrigger>().Initialize(_saveLoadService);
        }

        private void InitLevelTransferTriggers()
        {
            foreach (var saveTriggerObject in GameObject.FindGameObjectsWithTag(Tags.LevelTransferTrigger)) 
                saveTriggerObject.GetComponent<LevelTransferTrigger>().Initialize(_stateMachine);
        }

        private void InitEnemySpawners(LevelStaticData levelData)
        {
            Dictionary<string, Transform> spawnPoints = GetSpawnPoints();

            foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
            {
                if (spawnPoints.TryGetValue(spawnerData.SpawnerId, out Transform parent))
                    _gameFactory.CreateEnemySpawner(spawnerData.SpawnerId, spawnerData.EnemyTypeId, spawnerData.Position, parent);
                else
                    Debug.LogError($"Spawn point ID {spawnerData.SpawnerId} not found");
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

        private GameObject InitHero(LevelStaticData levelData)
        {
            GameObject hero =  _gameFactory.CreateHero(
                position: levelData.PlayerInitialPoint,
                parent: GameObject.FindWithTag(Tags.Game));
            return hero;
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud(parent: GameObject.FindWithTag(Tags.UI));
            
            HealthBarView healthBarView = hud.GetComponentInChildren<HealthBarView>();
            healthBarView.Initialize(hero.GetComponent<HeroHealth>());
        }

        private static Dictionary<string, Transform> GetSpawnPoints()
        {
            Dictionary<string, Transform> spawnPoints = new Dictionary<string, Transform>();
            SpawnPoint[] spawnPointObjects = Object.FindObjectsOfType<SpawnPoint>();
            
            foreach (var spawnPoint in spawnPointObjects)
            {
                UniqueId uniqueId = spawnPoint.GetComponent<UniqueId>();
                
                if (uniqueId != null) 
                    spawnPoints.Add(uniqueId.Id, spawnPoint.transform);
            }
            
            return spawnPoints;
        }

        private LevelStaticData GetLevelData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
            return levelData;
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