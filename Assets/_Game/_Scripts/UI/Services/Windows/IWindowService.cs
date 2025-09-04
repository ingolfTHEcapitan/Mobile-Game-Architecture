using _Game._Scripts.Infrastructure.Services;

namespace _Game._Scripts.UI.Services.Windows
{
    public interface IWindowService: IService
    {
        void Open(WindowId windowId);
    }
}