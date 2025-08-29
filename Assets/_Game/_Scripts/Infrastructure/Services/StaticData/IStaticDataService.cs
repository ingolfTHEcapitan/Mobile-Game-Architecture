using _Game._Scripts.StaticData;

namespace _Game._Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService: IService
    {
        void LoadEnemies();
        void LoadLevels();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}