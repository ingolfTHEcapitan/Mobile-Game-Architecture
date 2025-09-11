using System.Collections.Generic;
using _Game._Scripts.Data;
using _Game._Scripts.Enemy;
using _Game._Scripts.Hero;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.Input;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.Logic.EnemySpawner;
using _Game._Scripts.StaticData;
using _Game._Scripts.UI.Elements;
using _Game._Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;

namespace _Game._Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistantProgressService _progressService;
        private readonly IInputService _inputService;
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; private set; }

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
            HeroGameObject = InstantiateRegistered(AssetPath.Hero, position);
            HeroGameObject.SetParent(parent);
            HeroGameObject.GetComponent<HerroAttack>().Initialize(_inputService);
            return HeroGameObject;
        }

        public GameObject CreateHud(GameObject parent)
        {
            GameObject hud = InstantiateRegistered(AssetPath.Hud).SetParent(parent);
            
            LootCounter lootCounter = hud.GetComponentInChildren<LootCounter>();
            lootCounter.Initialize(_progressService.Progress.WorldData);
            
            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Initialize(_windowService);
            
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
            healthBarView.Initialize(health);
            healthBarView.UpdateHealthBar();

            LootSpawner lootSpawner = enemy.GetComponentInChildren<LootSpawner>();
            lootSpawner.Initialize(this);
            lootSpawner.SetLoot(data.MinLoot, data.MaxLoot);
            
            enemy.GetComponent<AgentMoveToPlayer>().Initialize(HeroGameObject.transform);
            enemy.GetComponent<NavMeshAgent>().speed = data.MoveSpeed;

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.Initialize(HeroGameObject.transform);
            attack.Damage = data.AttackDamage;
            attack.Cooldown = data.AttackCooldown;
            attack.Distance = data.AttackDistance;
            attack.Radius = data.AttackRadius;

            enemy.GetComponent<AgentRotateToPlayer>()?.Initialize(HeroGameObject.transform);

            return enemy;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();
            lootPiece.Initialize(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public void CreateEnemySpawner(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position)
        {
            EnemySpawner spawner = InstantiateRegistered(AssetPath.EnemySpawner, position).GetComponent<EnemySpawner>();
            
            spawner.Initialize(this);
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