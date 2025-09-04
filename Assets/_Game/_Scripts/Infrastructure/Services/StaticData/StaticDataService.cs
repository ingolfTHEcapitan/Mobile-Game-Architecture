using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.StaticData;
using _Game._Scripts.StaticData.Windows;
using _Game._Scripts.UI.Services.Windows;
using UnityEngine;

namespace _Game._Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataPath = "Configs/Enemys";
        private const string LevelStaticDataPath = "Configs/Levels";
        private const string WindowStaticDataPath = "Configs/UI/WindowStaticData";
        
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies = new Dictionary<EnemyTypeId, EnemyStaticData>();
        private Dictionary<string, LevelStaticData> _levels = new Dictionary<string, LevelStaticData>();
        private Dictionary<WindowId, WindowConfig> _windowConfigs = new Dictionary<WindowId, WindowConfig>();

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>(EnemyStaticDataPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }
        
        public void LoadLevels()
        {
            _levels = Resources.LoadAll<LevelStaticData>(LevelStaticDataPath)
                .ToDictionary(x => x.SceneKey, x => x);
        }
        
        public void LoadWindows()
        {
            WindowStaticData windowStaticData = Resources.Load<WindowStaticData>(WindowStaticDataPath);
            List<WindowConfig> windowConfigs = windowStaticData.Configs;
            _windowConfigs = windowConfigs.ToDictionary(x => x.WindowId, x => x);
        }
        
        public EnemyStaticData ForEnemy(EnemyTypeId typeId)
        {
            if (_enemies.TryGetValue(typeId, out EnemyStaticData enemyStaticData))
                return enemyStaticData;

            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData spawnerStaticData))
                return spawnerStaticData;

            return null;
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            if (_windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig))
                return windowConfig;

            return null;
        }
    }
}