using System.Collections.Generic;
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
        GameObject CreateHero(Vector3 position, GameObject parent);
        GameObject CreateHud(GameObject parent);
        GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform transform);
        LootPiece CreateLoot();
        void CleanupProgressReadersWriters();
        void CreateEnemySpawner(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position, Transform parent);
    }
}