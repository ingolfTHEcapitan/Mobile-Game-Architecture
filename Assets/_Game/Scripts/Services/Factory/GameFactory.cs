using System.Collections.Generic;
using _Game.Scripts.Data;
using _Game.Scripts.Logic.Enemy;
using _Game.Scripts.Logic.Enemy.Attacking;
using _Game.Scripts.Logic.Enemy.Loot;
using _Game.Scripts.Logic.Enemy.Movement;
using _Game.Scripts.Logic.EnemySpawner;
using _Game.Scripts.Logic.Hero;
using _Game.Scripts.Services.AssetManagement;
using _Game.Scripts.Services.Input;
using _Game.Scripts.Services.PersistantProgress;
using _Game.Scripts.Services.StaticData;
using _Game.Scripts.StaticData;
using _Game.Scripts.UI.Elements;
using _Game.Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistantProgressService _progressService;
        private readonly IInputService _inputService;
        private readonly IWindowService _windowService;

        private GameObject _heroGameObject { get; set; }
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, 
            IPersistantProgressService progressService, IInputService inputService, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _inputService = inputService;
            _windowService = windowService;
        }

        public GameObject CreateHero(Vector3 position, GameObject parent)
        {
            _heroGameObject = InstantiateRegistered(AssetPath.Hero, position);
            _heroGameObject.SetParent(parent);
            HerroAttack herroAttack = _heroGameObject.GetComponent<HerroAttack>();
            herroAttack.Construct(_inputService);
            herroAttack.Initialize();
            _heroGameObject.GetComponent<HeroMove>().Construct(_inputService);
            return _heroGameObject;
        }

        public GameObject CreateHud(GameObject parent)
        {
            GameObject hud = InstantiateRegistered(AssetPath.Hud).SetParent(parent);
            
            LootCounter lootCounter = hud.GetComponentInChildren<LootCounter>();
            lootCounter.Construct(_progressService.Progress.WorldData);
            lootCounter.Initialize();
            
            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);
            
            return hud;
        }

        public GameObject CreateEnemy(EnemyTypeId typeId, Transform parent)
        {
            EnemyStaticData data = _staticData.ForEnemy(typeId);
            GameObject enemy = Object.Instantiate(data.Model, parent.position, Quaternion.identity, parent);
            
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.Max = data.Health;
            health.Current = data.Health;

            HealthBarView healthBarView = enemy.GetComponent<HealthBarView>();
            healthBarView.Construct(health);
            healthBarView.UpdateHealthBar();

            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(factory: this);
            lootSpawner.SetLoot(data.MinLoot, data.MaxLoot);
            
            enemy.GetComponent<AgentMoveToPlayer>().Construct(_heroGameObject.transform);
            enemy.GetComponent<NavMeshAgent>().speed = data.MoveSpeed;

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.Construct(_heroGameObject.transform);
            attack.Damage = data.AttackDamage;
            attack.Cooldown = data.AttackCooldown;
            attack.Distance = data.AttackDistance;
            attack.Radius = data.AttackRadius;

            enemy.GetComponent<AgentRotateToPlayer>()?.Construct(_heroGameObject.transform);
            return enemy;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public void CreateEnemySpawner(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position, Transform parent)
        {
            GameObject spawnerObject = InstantiateRegistered(AssetPath.EnemySpawner, position).SetParent(parent.gameObject);
            EnemySpawner spawner = spawnerObject.GetComponent<EnemySpawner>();
            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.EnemyTypeId = enemyTypeId;
        }

        public void CleanupProgressReadersWriters()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void RegisterProgressReaders(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                RegisterProgressWriters(progressReader);
        }

        private void RegisterProgressWriters(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriters)
                ProgressWriters.Add(progressWriters);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressReaders(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressReaders(gameObject);
            return gameObject;
        }
    }
}