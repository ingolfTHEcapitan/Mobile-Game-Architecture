namespace _Game._Scripts.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();
        
        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }
        
        // При таком подходе через дженерики
        // внутри для каждого сервиса создастся отдельный экземпляр класса.
        private static class  Implementation<TService>  where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}