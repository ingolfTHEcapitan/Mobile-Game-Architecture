using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.Logic.Common;
using _Game.Scripts.Logic.Enemy.Loot;
using _Game.Scripts.Logic.EnemySpawner;
using _Game.Scripts.Logic.Hero;
using _Game.Scripts.Logic.Triggers;
using _Game.Scripts.Services.AssetManagement;
using _Game.Scripts.Services.Factory;
using _Game.Scripts.Services.PersistantProgress;
using _Game.Scripts.Services.SaveLoad;
using _Game.Scripts.Services.StaticData;
using _Game.Scripts.StaticData;
using _Game.Scripts.UI.Elements;
using _Game.Scripts.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Infrastructure.States.GameStates
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
            _gameFactory.CleanUp();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private async void OnLoaded()
        {
            await InitPopUpLayer();
            await InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitPopUpLayer() => 
           await _uiFactory.CreatePopUpLayer();

        private async Task InitGameWorld()
        {
            LevelStaticData levelData = GetLevelData();
            InitSaveTriggers();
            InitLevelTransferTriggers();
            await InitEnemySpawners(levelData);
            await InitLootPieces();

            GameObject hero = await InitHero(levelData);
            await InitHud(hero);
            CameraFollow(hero);
        }

        private void InitSaveTriggers()
        {
            foreach (var saveTriggerObject in GameObject.FindGameObjectsWithTag(Tags.SaveTrigger)) 
                saveTriggerObject.GetComponent<SaveTrigger>().Construct(_saveLoadService);
        }

        private void InitLevelTransferTriggers()
        {
            foreach (var saveTriggerObject in GameObject.FindGameObjectsWithTag(Tags.LevelTransferTrigger)) 
                saveTriggerObject.GetComponent<LevelTransferTrigger>().Construct(_stateMachine);
        }

        private async Task InitEnemySpawners(LevelStaticData levelData)
        {
            Dictionary<string, Transform> spawnPoints = GetSpawnPoints();

            foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
            {
                if (spawnPoints.TryGetValue(spawnerData.SpawnerId, out Transform parent))
                    await _gameFactory.CreateEnemySpawner(spawnerData.SpawnerId, spawnerData.EnemyTypeId, spawnerData.Position, parent);
                else
                    Debug.LogError($"Spawn point ID {spawnerData.SpawnerId} not found");
            }
        }

        private async Task InitLootPieces()
        {
            foreach (string key in _progressService.Progress.WorldData.LootData.LootPiecesOnScene.Dictionary.Keys)
            {
                LootPiece lootPiece = await _gameFactory.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = key;
            }
        }

        private async Task<GameObject> InitHero(LevelStaticData levelData)
        {
            GameObject hero =  await _gameFactory.CreateHero(
                position: levelData.PlayerInitialPoint,
                parent: GameObject.FindWithTag(Tags.Game));
            return hero;
        }

        private async Task InitHud(GameObject hero)
        {
            GameObject hud = await _gameFactory.CreateHud(parent: GameObject.FindWithTag(Tags.UI));
            
            HealthBarView healthBarView = hud.GetComponentInChildren<HealthBarView>();
            healthBarView.Construct(hero.GetComponent<HeroHealth>());
            healthBarView.Initialize();
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
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void CameraFollow(GameObject target) => 
            Camera.main.GetComponent<CameraFollow>().Follow(target);
    }
}