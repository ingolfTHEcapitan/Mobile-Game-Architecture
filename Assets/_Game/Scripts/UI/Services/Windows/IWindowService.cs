using _Game.Scripts.Services;

namespace _Game.Scripts.UI.Services.Windows
{
    public interface IWindowService: IService
    {
        void Open(WindowId windowId);
    }
}