using _Game.Scripts.StaticData;
using _Game.Scripts.StaticData.Windows;
using _Game.Scripts.UI.Services.Windows;

namespace _Game.Scripts.Services.StaticData
{
    public interface IStaticDataService: IService
    {
        void LoadEnemies();
        void LoadLevels();
        void LoadWindows();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId shop);
    }
}