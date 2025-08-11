using System;
using System.Collections.Generic;
using _Game._Scripts.Data;
using _Game._Scripts.Enemy;
using _Game._Scripts.Infrastructure.AssetManagement;
using _Game._Scripts.Infrastructure.Services;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.UI;
using UnityEngine;
using UnityEngine.AI;

namespace _Game._Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; private set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
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
            EnemyStaticData monsterData = _staticData.GetEnemyStaticData(typeId);
            GameObject enemy = UnityEngine.Object.Instantiate(monsterData.Model, parent.position, Quaternion.identity, parent);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.Current = monsterData.Helth;
            health.Max = monsterData.Helth;

            enemy.GetComponent<ActorUI>().Initialize(health);
            enemy.GetComponent<AgentMoveToPlayer>().Initialize(HeroGameObject.transform);
            enemy.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.Initialize(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.AttackCooldown = monsterData.AttackCooldown;
            attack.AttackDistance = monsterData.AttackDistance;
            attack.AttackRadius = monsterData.AttackRadius;

            enemy.GetComponent<AgentRotateToPlayer>()?.Initialize(HeroGameObject.transform);

            return enemy;
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