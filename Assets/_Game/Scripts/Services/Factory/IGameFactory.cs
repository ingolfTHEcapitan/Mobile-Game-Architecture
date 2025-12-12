using System.Collections.Generic;
using System.Threading.Tasks;
using _Game.Scripts.Logic.Enemy.Loot;
using _Game.Scripts.Services.PersistantProgress;
using _Game.Scripts.StaticData;
using UnityEngine;

namespace _Game.Scripts.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        Task<GameObject> CreateHero(Vector3 position, GameObject parent);
        Task WarmUp();
        Task<GameObject> CreateHud(GameObject parent);
        Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId, Transform transform);
        Task<LootPiece> CreateLoot();
        Task CreateEnemySpawner(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position, Transform parent);
        void CleanUp();
    }
}