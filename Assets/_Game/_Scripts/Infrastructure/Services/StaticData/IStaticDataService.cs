using _Game._Scripts.StaticData;
using _Game._Scripts.StaticData.Windows;
using _Game._Scripts.UI.Services.Windows;

namespace _Game._Scripts.Infrastructure.Services.StaticData
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