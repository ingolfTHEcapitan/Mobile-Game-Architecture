using _Game._Scripts.StaticData;

namespace _Game._Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService: IService
    {
        EnemyStaticData GetEnemyStaticData(EnemyesTypeId typeId);
        void LoadEnemys();
    }
}