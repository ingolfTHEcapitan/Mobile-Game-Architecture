using System;
using _Game._Scripts.StaticData;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.StaticData
{
    [Serializable]
    public class EnemySpawnerStaticData
    {
        public string SpawnerId;
        public EnemyTypeId EnemyTypeId;
        public Vector3 Position;

        public EnemySpawnerStaticData(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position)
        {
            SpawnerId = spawnerId;
            EnemyTypeId = enemyTypeId;
            Position = position;
        }
    }
}