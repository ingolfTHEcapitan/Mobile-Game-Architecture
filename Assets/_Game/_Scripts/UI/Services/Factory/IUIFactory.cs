using _Game._Scripts.Data;
using _Game._Scripts.Infrastructure.Services;

namespace _Game._Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreatePopUpLayer();
    }
}