using System.Collections.Generic;
using _Game._Scripts.Data;
using _Game._Scripts.Enemy;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.StaticData;
using _Game._Scripts.UI;
using UnityEngine;
using UnityEngine.AI;

namespace _Game._Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistantProgressService _progressService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IPersistantProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public GameObject CreateHero(GameObject at, GameObject parent)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroGameObject.SetParent(parent);
            return HeroGameObject;
        }

        public GameObject CreateHud(GameObject parent)
        {
            return InstantiateRegistered(AssetPath.HudPath).SetParent(parent);
        }

        public GameObject CreateEnemy(EnemyesTypeId typeId, Transform parent)
        {
            EnemyStaticData data = _staticData.GetEnemyStaticData(typeId);
            GameObject enemy = Object.Instantiate(data.Model, parent.position, Quaternion.identity, parent);
            
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.Max = data.Health;
            health.Current = data.Health;

            ActorUI actorUI = enemy.GetComponent<ActorUI>();
            actorUI.Initialize(health);
            actorUI.UpdateHealthBar();

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

        public LootPiece Createoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.LootPath).GetComponent<LootPiece>();
            lootPiece.Initialize(_progressService.Progress.WorldData);
            return lootPiece;
        }
        
        public void CleanupProgressReadersWriters()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void RegisterProgressReaders(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                RegisterProgressWriters(progressReader);
        }

        public void RegisterProgressWriters(ISavedProgressReader progressReader)
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