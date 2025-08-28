using System.Collections.Generic;
using _Game._Scripts.Enemy;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.StaticData;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HeroGameObject { get; }
        GameObject CreateHero(GameObject at, GameObject parent);
        GameObject CreateHud(GameObject parent);
        GameObject CreateEnemy(EnemyesTypeId enemyeTypeId, Transform transform);
        LootPiece CreateLoot();
        void CleanupProgressReadersWriters();
        void RegisterProgressWriters(ISavedProgressReader progressReader);
        void RegisterProgressReaders(GameObject gameObject);
    }
}