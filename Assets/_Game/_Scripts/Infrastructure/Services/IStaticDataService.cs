namespace _Game._Scripts.Infrastructure.Services
{
    public interface IStaticDataService: IService
    {
        EnemyStaticData GetEnemyStaticData(EnemyesTypeId typeId);
        void LoadEnemys();
    }
}