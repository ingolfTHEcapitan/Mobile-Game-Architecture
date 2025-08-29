using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.StaticData;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataPath = "Configs/Enemys";
        private const string LevelStaticDataPath = "Configs/Levels";

        private Dictionary<EnemyTypeId, EnemyStaticData> _enemys = new Dictionary<EnemyTypeId, EnemyStaticData>();
        private Dictionary<string, LevelStaticData> _levels = new Dictionary<string, LevelStaticData>();

        public void LoadEnemies()
        {
            _enemys = Resources.LoadAll<EnemyStaticData>(EnemyStaticDataPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }
        
        public void LoadLevels()
        {
            _levels = Resources.LoadAll<LevelStaticData>(LevelStaticDataPath)
                .ToDictionary(x => x.SceneKey, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            if (_enemys.TryGetValue(typeId, out EnemyStaticData enemyStaticData))
                return enemyStaticData;

            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData spawnerStaticData))
                return spawnerStaticData;

            return null;
        }
    }
}