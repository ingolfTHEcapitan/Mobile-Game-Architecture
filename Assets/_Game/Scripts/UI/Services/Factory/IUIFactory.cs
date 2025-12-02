using _Game.Scripts.Services;

namespace _Game.Scripts.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreatePopUpLayer();
    }
}