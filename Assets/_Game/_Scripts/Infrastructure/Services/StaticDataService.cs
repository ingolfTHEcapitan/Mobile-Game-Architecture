using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services
{
    class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataPath = "Configs/Enemys";

        private Dictionary<EnemyesTypeId, EnemyStaticData> _enemys = new Dictionary<EnemyesTypeId, EnemyStaticData>();

        public void LoadEnemys()
        {
            _enemys = Resources.LoadAll<EnemyStaticData>(EnemyStaticDataPath).ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public EnemyStaticData GetEnemyStaticData(EnemyesTypeId typeId)
        {
            if (_enemys.TryGetValue(typeId, out EnemyStaticData enemyStaticData))
                return enemyStaticData;

            return null;
        }
    }
}